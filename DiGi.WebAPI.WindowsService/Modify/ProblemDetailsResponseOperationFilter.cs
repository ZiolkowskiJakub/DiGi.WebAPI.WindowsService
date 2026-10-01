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
        /// <summary>
        /// Points the <c>application/json</c> content of every 4xx and 5xx response that has content at the <c>ProblemDetails</c> component.
        /// </summary>
        /// <param name="operation">The OpenAPI operation to be modified.</param>
        /// <param name="context">The context carrying the document the schema reference resolves against.</param>
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

                    // Set the schema to a reference to the ProblemDetails component
                    var mediaType = response.Content["application/json"];
                    mediaType.Schema = new OpenApiSchemaReference("ProblemDetails", context.Document);
                }
            }
        }
    }
}
