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
        /// <summary>
        /// Initializes the specified assembly by scanning for classes inheriting from <see cref="WebAPIController"/> 
        /// and executing any static initialization methods named "Initialize" or "InitializeAsync" within a class named "Modify".
        /// </summary>
        /// <param name="assembly">The assembly to be scanned for controllers and initialization logic.</param>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> used to register services and controllers.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if 
        /// controllers were registered or initialization methods were successfully executed; otherwise, <see langword="false"/>.</returns>
        public static async Task<bool> InitializeAsync(this Assembly? assembly, IServiceCollection? serviceCollection)
        {
            if (assembly is null || serviceCollection is null)
            {
                return false;
            }

            IMvcBuilder mvcBuilder = serviceCollection.AddControllers();

            bool result = false;

            bool containsController = false;
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    // Check for custom controllers
                    if (type.IsSubclassOf(typeof(WebAPIController)))
                    {
                        containsController = true;
                        Serilog.Modify.Log("WebAPIController found: {Name}", type.Name);
                    }

                    // Static initialization pattern
                    if (type.Name == "Modify")
                    {
                        List<MethodInfo?> methodInfos = [];
                        methodInfos.Add(type.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static));
                        methodInfos.Add(type.GetMethod("InitializeAsync", BindingFlags.Public | BindingFlags.Static));

                        methodInfos.RemoveAll(x => x is null);

                        if (methodInfos.Count > 0)
                        {
                            Serilog.Modify.Log("Initialize methods found");

                            foreach (MethodInfo? methodInfo in methodInfos)
                            {
                                if (methodInfo is null)
                                {
                                    continue; ;
                                }

                                if (methodInfo == null || methodInfo.GetParameters().Length != 1 || methodInfo.GetParameters()[0].ParameterType != typeof(IServiceCollection))
                                {
                                    Serilog.Modify.Log("Method initialization skipped : {Name}", methodInfo?.Name ?? "???");
                                    continue;
                                }

                                Serilog.Modify.Log("Method initialization started : {Name}", methodInfo.Name);

                                var result_Invoke = methodInfo.Invoke(null, [serviceCollection]);

                                // If the method is async (returns Task or ValueTask), we must await it
                                if (result_Invoke is Task task)
                                {
                                    await task;
                                }
                                else if (result_Invoke is ValueTask valueTask)
                                {
                                    await valueTask;
                                }

                                Serilog.Modify.Log("Method initialization ended");

                                result = true;
                            }
                        }
                    }
                }

                if (containsController)
                {
                    Serilog.Modify.Log("Adding application part: {AssemblyName}", assembly.FullName ?? "???");
                    // Register controllers from this assembly
                    mvcBuilder.AddApplicationPart(assembly);

                    result = true;
                }
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                // Log details about which types failed to load
                foreach (Exception? loaderException in reflectionTypeLoadException.LoaderExceptions)
                {
                    Serilog.Modify.Log(loaderException, "Error when initializing assembly");
                    //Console.WriteLine($"Type load error: {loaderException?.Message}");
                }
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "Error when initializing assembly");
            }

            return result;
        }
    }
}