using Microsoft.OpenApi;
using System.Collections.Generic;

namespace DiGi.WebAPI.WindowsService.Classes
{
    /// <summary>
    /// Collects the id of every schema component referenced in a walked OpenAPI document - from operations, parameters, request bodies, responses and from inside other components (<c>properties</c>, <c>items</c>, <c>allOf</c>, ...).
    /// <para>Overrides <c>Visit(IOpenApiReferenceHolder)</c>: the walker reports a <c>$ref</c> there, while <c>Visit(IOpenApiSchema)</c> never sees one (measured with Microsoft.OpenApi 2.7.5, 2026-10-01).</para>
    /// </summary>
    public class SchemaReferenceVisitor : OpenApiVisitorBase
    {
        /// <summary>
        /// Gets the ids of the referenced schema components collected so far.
        /// </summary>
        public HashSet<string> Ids { get; } = [];

        /// <summary>
        /// Records the component id of a schema reference.
        /// </summary>
        /// <param name="referenceHolder">The reference being visited.</param>
        public override void Visit(IOpenApiReferenceHolder referenceHolder)
        {
            if (referenceHolder is OpenApiSchemaReference openApiSchemaReference && openApiSchemaReference.Reference.Id is string id)
            {
                Ids.Add(id);
            }
        }
    }
}
