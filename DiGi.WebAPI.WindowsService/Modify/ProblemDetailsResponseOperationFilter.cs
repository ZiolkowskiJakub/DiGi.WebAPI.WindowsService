using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DiGi.WebAPI.WindowsService.Modify
{
    /// <summary>
    /// Ensures that all 4xx and 5xx responses use the ProblemDetails schema.
    /// </summary>
    public class ProblemDetailsResponseOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses == null) return;

            foreach (var kvp in operation.Responses)
            {
                var status = kvp.Key;
                // Check if status code is three digits and starts with 4 or 5
                if (status.Length == 3 && char.IsDigit(status[0]) && (status[0] == '4' || status[0] == '5'))
                {
                    var response = kvp.Value;
                    // Ensure there is at least an application/json media type
                    if (response.Content == null)
                    {
                        // Skip responses without content (e.g., 204)
                        continue;
                    }

                    if (!response.Content.ContainsKey("application/json"))
                    {
                        response.Content["application/json"] = new OpenApiMediaType();
                    }

                    // Set the schema to ProblemDetails reference
                    var mediaType = response.Content["application/json"];
                    mediaType.Schema = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "ProblemDetails" }
                    };
                }
            }
        }
    }
}