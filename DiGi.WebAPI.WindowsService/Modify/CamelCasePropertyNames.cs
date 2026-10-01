using Microsoft.OpenApi;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        /// <summary>
        /// Renames the properties of a schema, and its <c>required</c> entries, to camelCase - the names the host's MVC JSON options write (<see cref="ConfigureJsonSerializerOptions"/>).
        /// <para>Needed for payloads written by the MVC formatter only: Swashbuckle documents a <c>[JsonPropertyName]</c> as spelled, while the host's <c>ForceCamelCaseModifier</c> writes every MVC member camelCase regardless. Never apply it to a DiGi <c>ISerializableObject</c> payload, which the DiGi serializer writes under its exact names.</para>
        /// </summary>
        /// <param name="openApiSchema">The schema to rename the properties of.</param>
        public static void CamelCasePropertyNames(this OpenApiSchema? openApiSchema)
        {
            if (openApiSchema?.Properties is null)
            {
                return;
            }

            Dictionary<string, IOpenApiSchema> properties = [];
            foreach (KeyValuePair<string, IOpenApiSchema> keyValuePair in openApiSchema.Properties)
            {
                properties[JsonNamingPolicy.CamelCase.ConvertName(keyValuePair.Key)] = keyValuePair.Value;
            }

            openApiSchema.Properties = properties;

            if (openApiSchema.Required is not null)
            {
                openApiSchema.Required = new HashSet<string>(openApiSchema.Required.Select(JsonNamingPolicy.CamelCase.ConvertName));
            }
        }
    }
}
