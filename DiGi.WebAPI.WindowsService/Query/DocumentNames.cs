using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the names of the per-route-prefix Swagger documents: the distinct first route segments of every described endpoint.
        /// <para>The name of the full document (<see cref="Constants.Name.SwaggerDocument_Full"/>) is excluded, so a route prefix of that name cannot replace it.</para>
        /// </summary>
        /// <param name="apiDescriptionGroupCollectionProvider">The provider of the API descriptions of every loaded controller, extensions included.</param>
        /// <returns>The document names in ordinal order; empty when the provider is <c>null</c>.</returns>
        public static List<string> DocumentNames(this IApiDescriptionGroupCollectionProvider? apiDescriptionGroupCollectionProvider)
        {
            if (apiDescriptionGroupCollectionProvider is null)
            {
                return [];
            }

            IEnumerable<string> documentNames = apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items
                .SelectMany(apiDescriptionGroup => apiDescriptionGroup.Items)
                .Select(apiDescription => apiDescription.DocumentName())
                .OfType<string>()
                .Where(documentName => documentName != Constants.Name.SwaggerDocument_Full)
                .Distinct(StringComparer.Ordinal)
                .Order(StringComparer.Ordinal);

            return [.. documentNames];
        }
    }
}
