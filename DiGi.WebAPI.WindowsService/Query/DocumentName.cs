using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the name of the Swagger document an API description belongs to: the first segment of its relative path, in lower case.
        /// <para>The segment is lower-cased explicitly because the <c>[controller]</c> route token expands to the controller name as declared (<c>User</c>), while <c>LowercaseUrls</c> only affects generated links.</para>
        /// </summary>
        /// <param name="apiDescription">The API description to classify.</param>
        /// <returns>The lower-case first route segment (for example <c>gis</c> for <c>gis/Building2D/items</c>), or <c>null</c> when the description or its relative path is missing or the first segment is a route parameter (<c>{id}</c>); such an endpoint appears in the full document only.</returns>
        public static string? DocumentName(this ApiDescription? apiDescription)
        {
            string? relativePath = apiDescription?.RelativePath;
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return null;
            }

            string segment = relativePath.TrimStart('/').Split('/', 2)[0];
            if (string.IsNullOrWhiteSpace(segment) || segment.StartsWith('{'))
            {
                return null;
            }

            return segment.ToLowerInvariant();
        }
    }
}
