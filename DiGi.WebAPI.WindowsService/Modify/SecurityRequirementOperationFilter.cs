using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DiGi.WebAPI.WindowsService.Modify
{
    /// <summary>
    /// Adds an API key security requirement to operations that declare a 401 response.
    /// </summary>
    public class SecurityRequirementOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // If the operation already has a 401 response, attach the API key security requirement.
            if (operation.Responses?.ContainsKey("401") == true)
            {
                if (operation.Security == null)
                    operation.Security = new List<OpenApiSecurityRequirement>();

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "apiKey" } },
                        Array.Empty<string>()
                    }
                });
            }
        }
    }
}