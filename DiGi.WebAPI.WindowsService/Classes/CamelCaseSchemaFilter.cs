using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Text.Json;

namespace DiGi.WebAPI.WindowsService.Classes
{
    /// <summary>
    /// Provides a schema filter that converts property names in the OpenAPI schema to camelCase.
    /// </summary>
    public class CamelCaseSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the camelCase naming policy to the properties of the provided OpenAPI schema.
        /// </summary>
        /// <param name="schema">The OpenAPI schema to be modified.</param>
        /// <param name="context">The context containing information about the schema being filtered.</param>
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            if (schema is not OpenApiSchema openApiSchema)
            {
                return;
            }

            var newProperties = new Dictionary<string, IOpenApiSchema>();
            foreach (var property in schema.Properties)
            {
                var camelCaseName = JsonNamingPolicy.CamelCase.ConvertName(property.Key);
                newProperties[camelCaseName] = property.Value;
            }
            openApiSchema.Properties = newProperties;
        }
    }
}
