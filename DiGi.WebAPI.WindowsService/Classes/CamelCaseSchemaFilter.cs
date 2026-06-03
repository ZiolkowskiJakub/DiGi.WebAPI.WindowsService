using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Text.Json;

namespace DiGi.WebAPI.WindowsService.Classes
{
    public class CamelCaseSchemaFilter : ISchemaFilter
    {
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
