using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the version of the assembly whose controllers serve the endpoints of the given Swagger document.
        /// <para>When several assemblies share the route prefix, the highest version is returned.</para>
        /// </summary>
        /// <param name="apiDescriptionGroupCollectionProvider">The provider of the API descriptions of every loaded controller, extensions included.</param>
        /// <param name="documentName">The document name, as returned by <see cref="DocumentName(ApiDescription?)"/>.</param>
        /// <returns>The three-part assembly version (for example <c>0.8.8</c>), or <c>null</c> when no controller serves the document or its assembly carries no version.</returns>
        public static string? DocumentVersion(this IApiDescriptionGroupCollectionProvider? apiDescriptionGroupCollectionProvider, string? documentName)
        {
            if (apiDescriptionGroupCollectionProvider is null || string.IsNullOrWhiteSpace(documentName))
            {
                return null;
            }

            Version? version = apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items
                .SelectMany(apiDescriptionGroup => apiDescriptionGroup.Items)
                .Where(apiDescription => apiDescription.DocumentName() == documentName)
                .Select(apiDescription => (apiDescription.ActionDescriptor as ControllerActionDescriptor)?.ControllerTypeInfo.Assembly.GetName().Version)
                .OfType<Version>()
                .Max();

            return version?.ToString(3);
        }
    }
}
