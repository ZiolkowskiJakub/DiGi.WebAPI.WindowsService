using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DiGi.WebAPI.WindowsService.Classes
{
    /// <summary>
    /// Removes the enum components a generated document no longer references (<see cref="Modify.RemoveUnreferencedEnumSchemas"/>): those whose only users were DiGi payload members, which declare their enum inline.
    /// </summary>
    public class UnreferencedEnumDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Removes every enum component that nothing in the document references.
        /// </summary>
        /// <param name="swaggerDoc">The OpenAPI document to be modified.</param>
        /// <param name="context">The context of the document being generated.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.RemoveUnreferencedEnumSchemas();
        }
    }
}
