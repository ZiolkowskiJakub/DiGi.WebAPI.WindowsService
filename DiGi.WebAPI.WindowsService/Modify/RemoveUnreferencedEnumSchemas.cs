using DiGi.WebAPI.WindowsService.Classes;
using Microsoft.OpenApi;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        /// <summary>
        /// Removes every enum component that nothing in the document references.
        /// <para>A DiGi payload member declares its enum inline (<see cref="Create.OpenApiSchema"/>), but generating the member's schema still registers the shared component, which lists member names. An enum used only by DiGi payloads would therefore stay in the document unreferenced, advertising the string form its payloads do not carry; an enum referenced by a query parameter or an MVC payload is kept. Only enum components are removed - an unreferenced object component is left alone. See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6.</para>
        /// <para>The host registers it in <see cref="ConfigureSchemaGeneration"/>, ahead of any <c>IWebAPIDocumentFilter</c> an extension brings: such a filter adding a reference to an enum component would find it removed. No loaded extension has one (2026-10-01).</para>
        /// </summary>
        /// <param name="openApiDocument">The document to clean up.</param>
        public static void RemoveUnreferencedEnumSchemas(this OpenApiDocument? openApiDocument)
        {
            if (openApiDocument?.Components?.Schemas is not IDictionary<string, IOpenApiSchema> schemas || schemas.Count == 0)
            {
                return;
            }

            SchemaReferenceVisitor schemaReferenceVisitor = new();
            new OpenApiWalker(schemaReferenceVisitor).Walk(openApiDocument);

            // An enum references nothing, so removing one cannot leave another component unreferenced: one pass suffices.
            List<string> ids = [.. schemas.Where(keyValuePair => keyValuePair.Value.Enum is not null && keyValuePair.Value.Enum.Count != 0 && !schemaReferenceVisitor.Ids.Contains(keyValuePair.Key)).Select(keyValuePair => keyValuePair.Key)];
            foreach (string id in ids)
            {
                schemas.Remove(id);
            }
        }
    }
}
