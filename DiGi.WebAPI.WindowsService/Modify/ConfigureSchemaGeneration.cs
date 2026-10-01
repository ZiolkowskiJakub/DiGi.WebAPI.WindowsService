using DiGi.WebAPI.WindowsService.Classes;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        /// <summary>
        /// Applies the host's schema and parameter naming conventions to the Swagger generator: camelCase query parameters,
        /// the schema filter that shapes every payload schema, the integer values on the description of every enum
        /// parameter, and the removal of the enum components that only DiGi payloads used (ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6).
        /// <para>Kept apart from the document registration in <c>Program</c> so that tests generate schemas through exactly the configuration the host serves.</para>
        /// </summary>
        /// <param name="swaggerGenOptions">The Swagger generator options to configure.</param>
        public static void ConfigureSchemaGeneration(this SwaggerGenOptions? swaggerGenOptions)
        {
            if (swaggerGenOptions is null)
            {
                return;
            }

            swaggerGenOptions.DescribeAllParametersInCamelCase();
            swaggerGenOptions.SchemaFilter<WireFormatSchemaFilter>();
            swaggerGenOptions.OperationFilter<EnumParameterDescriptionFilter>();
            swaggerGenOptions.DocumentFilter<UnreferencedEnumDocumentFilter>();
        }
    }
}
