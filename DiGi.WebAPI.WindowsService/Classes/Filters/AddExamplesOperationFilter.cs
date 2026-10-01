using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

namespace DiGi.WebAPI.WindowsService.Classes.Filters
{
    /// <summary>
    /// Adds example values to parameters, request bodies, and responses in the OpenAPI document.
    /// <para>Sets a simple example on any schema that does not already have one: the inline schema itself, or, when the
    /// operation refers to a component, the component schema the reference points to - OpenAPI 3.0 ignores everything
    /// written beside a <c>$ref</c>, and a reference has no writable example of its own.</para>
    /// </summary>
    public class AddExamplesOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Sets a simple example on the schema of every parameter, request body, and response of the operation that has none.
        /// </summary>
        /// <param name="operation">The OpenAPI operation to be modified.</param>
        /// <param name="context">The context carrying the schema repository an operation's references resolve against.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation is null)
            {
                return;
            }

            foreach (IOpenApiParameter parameter in operation.Parameters ?? [])
            {
                ApplyExample(parameter.Schema, context);
            }

            if (operation.RequestBody?.Content is not null)
            {
                foreach (OpenApiMediaType mediaType in operation.RequestBody.Content.Values)
                {
                    ApplyExample(mediaType.Schema, context);
                }
            }

            if (operation.Responses is not null)
            {
                foreach (IOpenApiResponse response in operation.Responses.Values)
                {
                    if (response.Content is null)
                    {
                        continue;
                    }

                    foreach (OpenApiMediaType mediaType in response.Content.Values)
                    {
                        ApplyExample(mediaType.Schema, context);
                    }
                }
            }
        }

        // An inline schema takes its own example; a reference is followed to the component it points to, because the
        // reference exposes a read-only example and OpenAPI 3.0 rejects siblings beside a $ref.
        private static void ApplyExample(IOpenApiSchema? schema, OperationFilterContext? context)
        {
            OpenApiSchema? openApiSchema = schema as OpenApiSchema;

            if (openApiSchema is null
                && schema is OpenApiSchemaReference openApiSchemaReference
                && !string.IsNullOrWhiteSpace(openApiSchemaReference.Reference.Id)
                && context is not null
                && context.SchemaRepository.Schemas.TryGetValue(openApiSchemaReference.Reference.Id, out IOpenApiSchema? componentSchema))
            {
                openApiSchema = componentSchema as OpenApiSchema;
            }

            if (openApiSchema is not null && openApiSchema.Example is null)
            {
                SetSimpleExample(openApiSchema);
            }
        }

        // The v2 example is a JsonNode. Nullability is folded into Type (X | Null), so the Null flag is masked out
        // before the dispatch; an enum takes one of its own values, which the plain string fallback would violate.
        private static void SetSimpleExample(OpenApiSchema openApiSchema)
        {
            if (openApiSchema.Enum is not null)
            {
                foreach (JsonNode? jsonNode in openApiSchema.Enum)
                {
                    if (jsonNode is not null)
                    {
                        openApiSchema.Example = jsonNode.DeepClone();
                        return;
                    }
                }
            }

            JsonSchemaType type = (openApiSchema.Type ?? JsonSchemaType.Null) & ~JsonSchemaType.Null;

            if (type == JsonSchemaType.String)
            {
                openApiSchema.Example = string.IsNullOrWhiteSpace(openApiSchema.Format)
                    ? JsonValue.Create("example")
                    : JsonValue.Create(openApiSchema.Format.ToLowerInvariant() switch
                    {
                        "date" => "2026-01-01",
                        "date-time" => "2026-01-01T12:00:00Z",
                        "uuid" => "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                        _ => "example"
                    });
            }
            else if (type == JsonSchemaType.Integer)
            {
                openApiSchema.Example = JsonValue.Create(0);
            }
            else if (type == JsonSchemaType.Boolean)
            {
                openApiSchema.Example = JsonValue.Create(false);
            }
            else if (type == JsonSchemaType.Number)
            {
                openApiSchema.Example = JsonValue.Create(0.0);
            }
            else if (type == JsonSchemaType.Object)
            {
                // For objects, set an empty object as example
                openApiSchema.Example = new JsonObject();
            }
            else if (type == JsonSchemaType.Array)
            {
                // For arrays, set an empty array as example
                openApiSchema.Example = new JsonArray();
            }
            else
            {
                // Fallback: string example
                openApiSchema.Example = JsonValue.Create("example");
            }
        }
    }
}
