using System.Reflection;
using System.Runtime.Loader;

namespace DiGi.WebAPI.WindowsService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);
            IServiceCollection serviceCollection = webApplicationBuilder.Services;

            bool isDevelopment = webApplicationBuilder.Environment.IsDevelopment();

            // Route configuration
            serviceCollection.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            if (isDevelopment)
            {
                serviceCollection.AddEndpointsApiExplorer();
                serviceCollection.AddSwaggerGen();
            }

            // Cache for loaded dependencies to ensure we don't reload the same DLL multiple times
            Dictionary<string, Assembly> dictionary_LoadedAssembly = [];

            string? directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                List<string> paths = [.. Directory.GetFiles(directory, "*.dll").Where(path => !Query.ExcludedLibrary(path))];

                // We create a list of resolvers for all potential plugin locations
                IEnumerable<AssemblyDependencyResolver> assemblyDependencyResolvers = paths.Select(path => new AssemblyDependencyResolver(path));

                // Global handler - registered ONCE - that uses all available resolvers
                AssemblyLoadContext.Default.Resolving += (context, assemblyName) =>
                {
                    // 1. Check if already loaded in our cache
                    if (dictionary_LoadedAssembly.TryGetValue(assemblyName.FullName, out var existing))
                    {
                        return existing;
                    }

                    // 2. Check if already in the runtime
                    Assembly? assembly_AlreadyInRuntime = AssemblyLoadContext.Default.Assemblies.FirstOrDefault(a => a.GetName().Name == assemblyName.Name);
                    if (assembly_AlreadyInRuntime != null)
                    {
                        return assembly_AlreadyInRuntime;
                    }

                    // 3. Try to resolve using any of the available resolvers (from our DLLs)
                    foreach (AssemblyDependencyResolver assemblyDependencyResolver in assemblyDependencyResolvers)
                    {
                        string? assemblyPath = assemblyDependencyResolver.ResolveAssemblyToPath(assemblyName);
                        if (assemblyPath != null)
                        {
                            Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
                            dictionary_LoadedAssembly[assemblyName.FullName] = assembly;
                            return assembly;
                        }
                    }
                    return null;
                };

                // Now actually load the assemblies to find controllers
                foreach (string path in paths)
                {
                    try
                    {
                        // Use the default context to load the assembly
                        Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                        if(assembly is null)
                        {
                            continue;
                        }

                        await Modify.InitializeAsync(assembly, serviceCollection);
                    }
                    catch //(Exception ex)
                    {
                        //Console.WriteLine($"[Error] Could not load {Path.GetFileName(path)}: {ex.Message}");
                    }
                }
            }

            // --- END: Optimized Dynamic Loading Logic ---

            WebApplication webApplication = webApplicationBuilder.Build();

            // Configure the HTTP request pipeline.
            if (isDevelopment)
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI();
            }

            webApplication.UseAuthorization();
            webApplication.MapControllers();
            webApplication.Run();
        }
    }
}