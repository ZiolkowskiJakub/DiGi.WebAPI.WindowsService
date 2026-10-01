using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.WebAPI.WindowsService.Classes.Filters
{
    /// <summary>
    /// Adds example values to parameters, request bodies, and responses in the OpenAPI document.
    /// Sets a simple example for any schema that doesn't already have one.
    /// </summary>
    public class AddExamplesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null) return;

            // Add examples to parameters
            foreach (var parameter in operation.Parameters ?? Array.Empty<IOpenApiParameter>())
            {
                if (parameter.Schema != null && parameter.Schema.Example == null)
                {
                    SetSimpleExample(parameter.Schema);
                }
            }

            // Add examples to request body
            if (operation.RequestBody != null)
            {
                foreach (var mediaType in operation.RequestBody.Content.Values)
                {
                    if (mediaType.Schema != null && mediaType.Schema.Example == null)
                    {
                        SetSimpleExample(mediaType.Schema);
                    }
                }
            }

            // Add examples to responses
            if (operation.Responses != null)
            {
                foreach (var kvp in operation.Responses)
                {
                    var response = kvp.Value;
                    if (response.Content != null)
                    {
                        foreach (var mediaType in response.Content.Values)
                        {
                            if (mediaType.Schema != null && mediaType.Schema.Example == null)
                            {
                                SetSimpleExample(mediaType.Schema);
                            }
                        }
                    }
                }
            }
        }

        private static void SetSimpleExample(IOpenApiSchema schema)
        {
            // Set a simple example based on schema type
            if (schema.Type == JsonSchemaType.String)
            {
                if (!string.IsNullOrWhiteSpace(schema.Format))
                {
                    switch (schema.Format.ToLowerInvariant())
                    {
                        case "date":
                            schema.Example = new Microsoft.OpenApi.OpenApiString("2026-01-01");
                            break;
                        case "date-time":
                            schema.Example = new Microsoft.OpenApi.OpenApiString("2026-01-01T12:00:00Z");
                            break;
                        case "uuid":
                            schema.Example = new Microsoft.OpenApi.OpenApiString("3fa85f64-5717-4562-b3fc-2c963f66afa6");
                            break;
                        default:
                            schema.Example = new Microsoft.OpenApi.OpenApiString("example");
                            break;
                    }
                }
                else
                {
                    schema.Example = new Microsoft.OpenApi.OpenApiString("example");
                }
            }
            else if (schema.Type == JsonSchemaType.Integer || schema.Type == JsonSchemaType.Long)
            {
                schema.Example = new Microsoft.OpenApi.OpenApiInteger(0);
            }
            else if (schema.Type == JsonSchemaType.Boolean)
            {
                schema.Example = new Microsoft.OpenApi.OpenApiBoolean(false);
            }
            else if (schema.Type == JsonSchemaType.Number)
            {
                schema.Example = new OpenApiFloat(0f);
            }
            else if (schema.Type == JsonSchemaType.Object)
            {
                // For objects, set an empty object as example
                schema.Example = new Microsoft.OpenApi.OpenApiObject();
            }
            else if (schema.Type == JsonSchemaType.Array)
            {
                // For arrays, set an empty array as example
                schema.Example = new Microsoft.OpenApi.OpenApiArray();
            }
            else
            {
                // Fallback: string example
                schema.Example = new Microsoft.OpenApi.OpenApiString("example");
            }
        }
    }
}