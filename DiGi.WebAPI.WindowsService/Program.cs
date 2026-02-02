
using System.Reflection;

namespace DiGi.WebAPI.WindowsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

            IServiceCollection serviceCollection = webApplicationBuilder.Services;

            serviceCollection.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            bool isDevelopment = webApplicationBuilder.Environment.IsDevelopment();

            // Add services to the container.

            if (isDevelopment)
            {
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                serviceCollection.AddEndpointsApiExplorer();
                serviceCollection.AddSwaggerGen();
            }

            serviceCollection.AddControllers();

            string? directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                string[] paths = Directory.GetFiles(directory!, "*.dll");

                foreach (string path in paths)
                {
                    Assembly? assembly = null;

                    try
                    {
                        assembly = Assembly.LoadFrom(path);
                        if (assembly is not null)
                        {
                            bool contains = false;

                            Type[] types = assembly.GetTypes();
                            if (types != null)
                            {
                                foreach (Type type in types)
                                {
                                    if (type.IsSubclassOf(typeof(Classes.WebAPIController)))
                                    {
                                        // This assembly contains at least one controller
                                        contains = true;
                                        break;
                                    }
                                }
                            }

                            if(!contains)
                            {
                                assembly = null;
                            }
                        }
                    }
                    catch (BadImageFormatException)
                    {
                        // Not a .NET assembly
                    }
                    catch (FileLoadException)
                    {
                        // Already loaded or dependency issue
                    }

                    if (assembly == null)
                    {
                        continue;
                    }

                    serviceCollection.AddControllers().AddApplicationPart(assembly);
                }
            }


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
