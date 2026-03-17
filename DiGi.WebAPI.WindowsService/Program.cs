using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DiGi.WebAPI.WindowsService
{
    public class Program
    {
        public static async Task Install()
        {
            // Get the path of the current executable
            string? path = Process.GetCurrentProcess()?.MainModule?.FileName;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            // Use sc.exe to create the service
            // binPath must point to the .exe file
            ProcessStartInfo processStartInfo = new()
            {
                FileName = "sc.exe",
                Arguments = $"create {Constants.Name.Service} binPath= \"{path}\" start= auto",
                UseShellExecute = true,
                Verb = "runas" // Request administrator privileges
            };

            Process.Start(processStartInfo)?.WaitForExit();
            //Console.WriteLine("Service installed successfully.");
        }

        private static async Task Uninstall()
        {
            ProcessStartInfo processInfo = new()
            {
                FileName = "sc.exe",
                Arguments = $"delete {Constants.Name.Service}",
                UseShellExecute = true,
                Verb = "runas"
            };

            Process.Start(processInfo)?.WaitForExit();
        }

        public static async Task Run(string[] args)
        {
            string? path_Process = System.Environment.ProcessPath;
            if (string.IsNullOrWhiteSpace(path_Process))
            {
                return;
            }

            string directory_Main = Path.GetDirectoryName(path_Process)!;

            Directory.SetCurrentDirectory(directory_Main);

            WebApplicationOptions webOptions = new()
            {
                Args = args,
                ContentRootPath = directory_Main
            };

            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(webOptions);
            webApplicationBuilder.Host.UseWindowsService(windowsServiceLifetimeOptions =>
            {
                windowsServiceLifetimeOptions.ServiceName = Constants.Name.Service;
            });

            IServiceCollection serviceCollection = webApplicationBuilder.Services;
            serviceCollection.AddAuthentication();

            bool isDevelopment = webApplicationBuilder.Environment.IsDevelopment();

            // Route configuration
            serviceCollection.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.LowercaseUrls = true;
                routeOptions.LowercaseQueryStrings = true;
            });

            serviceCollection.Configure<KestrelServerOptions>(kestrelServerOptions =>
            {
                kestrelServerOptions.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB
            });

            serviceCollection.Configure<FormOptions>(formOptions =>
            {
                formOptions.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100 MB
            });

            //if (isDevelopment)
            {
                serviceCollection.AddEndpointsApiExplorer();
                serviceCollection.AddSwaggerGen();
            }

            serviceCollection.AddControllers();

            // Cache for loaded dependencies to ensure we don't reload the same DLL multiple times
            Dictionary<string, Assembly> dictionary_LoadedAssembly = [];

            string? directory_Assembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrWhiteSpace(directory_Assembly))
            {
                string directory_Extensions = Path.Combine(directory_Assembly, "extensions");
                if (Directory.Exists(directory_Extensions))
                {
                    string[] directories = Directory.GetDirectories(directory_Extensions);
                    foreach (string directory in directories)
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
                                if (assembly is null)
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
                }
            }

            // --- END: Optimized Dynamic Loading Logic ---

            WebApplication webApplication = webApplicationBuilder.Build();

            // Configure the HTTP request pipeline.
            //if (isDevelopment)
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI();
            }

            webApplication.UseAuthorization();
            webApplication.MapControllers();
            webApplication.Run();
        }

        public static async Task Main(string[] args)
        {
            if (args.Contains("--install"))
            {
                await Install();
                return;
            }

            if (args.Contains("--uninstall"))
            {
                await Uninstall();
                return;
            }

            await Run(args);
        }
    }
}