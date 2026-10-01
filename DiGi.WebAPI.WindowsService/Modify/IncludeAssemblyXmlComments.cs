using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DiGi.WebAPI.WindowsService.Modify
{
    public static partial class Modify
    {
        /// <summary>
        /// Attaches the XML documentation file found beside each of the given assemblies to the Swagger generator, so that
        /// operations, parameters and schemas carry the <c>&lt;summary&gt;</c> of the code they describe.
        /// <para>Dynamic assemblies and assemblies without a location are skipped, as are assemblies without a sibling <c>.xml</c> file; a file that fails to load is logged and skipped.</para>
        /// </summary>
        /// <param name="swaggerGenOptions">The Swagger generator options to configure.</param>
        /// <param name="assemblies">The assemblies whose documentation is attached.</param>
        public static void IncludeAssemblyXmlComments(this SwaggerGenOptions? swaggerGenOptions, IEnumerable<Assembly>? assemblies)
        {
            if (swaggerGenOptions is null || assemblies is null)
            {
                return;
            }

            foreach (Assembly assembly in assemblies)
            {
                if (assembly.IsDynamic || string.IsNullOrWhiteSpace(assembly.Location))
                {
                    continue;
                }

                string path_Xml = Path.ChangeExtension(assembly.Location, ".xml");

                if (File.Exists(path_Xml))
                {
                    try
                    {
                        swaggerGenOptions.IncludeXmlComments(path_Xml);
                        Serilog.Modify.Log("Swagger: Documentation attached for {AssemblyName}", assembly.GetName().Name ?? "???");
                    }
                    catch (Exception exception)
                    {
                        Serilog.Modify.Log(exception, "Swagger: Could not load XML for {Path}", path_Xml);
                    }
                }
            }
        }
    }
}
