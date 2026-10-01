using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DiGi.WebAPI.WindowsService.Modify
{
    /// <summary>
    /// Adds an API key security requirement to operations that declare a 401 response.
    /// </summary>
    public class SecurityRequirementOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Attaches the <c>apiKey</c> security requirement to the operation when it declares a 401 response.
        /// </summary>
        /// <param name="operation">The OpenAPI operation to be modified.</param>
        /// <param name="context">The context carrying the document the security scheme reference resolves against.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // If the operation already has a 401 response, attach the API key security requirement.
            if (operation.Responses?.ContainsKey("401") == true)
            {
                if (operation.Security == null)
                {
                    operation.Security = new List<OpenApiSecurityRequirement>();
                }

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("apiKey", context.Document),
                        []
                    }
                });
            }
        }
    }
}
