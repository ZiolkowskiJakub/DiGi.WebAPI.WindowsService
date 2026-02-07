using DiGi.WebAPI.Classes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        public static async Task InitializeAsync(this Assembly? assembly, IServiceCollection? serviceCollection)
        {
            if(assembly is null || serviceCollection is null)
            {
                return;
            }

            bool containsController = false;
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    // Check for custom controllers
                    if (type.IsSubclassOf(typeof(WebAPIController)))
                    {
                        containsController = true;
                    }

                    // Static initialization pattern
                    if (type.Name == "Modify")
                    {
                        List<MethodInfo?> methodInfos = [];
                        methodInfos.Add(type.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static));
                        methodInfos.Add(type.GetMethod("InitializeAsync", BindingFlags.Public | BindingFlags.Static));

                        foreach (MethodInfo? methodInfo in methodInfos)
                        {
                            if (methodInfo == null || methodInfo.GetParameters().Length != 1 || methodInfo.GetParameters()[0].ParameterType != typeof(IServiceCollection))
                            {
                                continue;
                            }

                            var result = methodInfo.Invoke(null, [serviceCollection]);

                            // If the method is async (returns Task or ValueTask), we must await it
                            if (result is Task task)
                            {
                                await task;
                            }
                            else if (result is ValueTask valueTask)
                            {
                                await valueTask;
                            }
                        }

                    }
                }

                if (containsController)
                {
                    // Register controllers from this assembly
                    serviceCollection.AddControllers().AddApplicationPart(assembly);
                }
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                // Log details about which types failed to load
                foreach (Exception? loaderException in reflectionTypeLoadException.LoaderExceptions)
                {
                    //Console.WriteLine($"Type load error: {loaderException?.Message}");
                }
            }
        }
    }
}