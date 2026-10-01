using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using System;
using System.Linq;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        /// <summary>
        /// Appends the integer values of the enum to the description of every operation parameter typed by an enum, and advises sending the integer.
        /// <para>The parameter keeps its schema - a reference to the shared component listing the member names - because that is accurate for binding: ASP.NET binds the member name and the integer alike. The integer is the stable half of that contract (<c>Coding - WebAPI Contracts.md</c>, "Send enum values as integers"): member names have been renamed (<c>Subdivison</c> to <c>Subdivision</c>, after which the old spelling is a 400) while the integers never moved. See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6.</para>
        /// <para>Works on the finished operation rather than per parameter because a parameter's own XML description is written by a parameter filter registered after the host's, which would overwrite text appended earlier.</para>
        /// </summary>
        /// <param name="openApiOperation">The operation whose parameters are described.</param>
        /// <param name="apiDescription">The API description of the operation, giving the CLR type of each parameter.</param>
        public static void UpdateEnumParameterDescriptions(this OpenApiOperation? openApiOperation, ApiDescription? apiDescription)
        {
            if (openApiOperation?.Parameters is null || apiDescription is null)
            {
                return;
            }

            foreach (IOpenApiParameter openApiParameter in openApiOperation.Parameters)
            {
                if (openApiParameter is not OpenApiParameter openApiParameter_Concrete)
                {
                    continue;
                }

                // Parameter names may have been camel-cased (DescribeAllParametersInCamelCase), which changes only the case.
                ApiParameterDescription? apiParameterDescription = apiDescription.ParameterDescriptions.FirstOrDefault(apiParameterDescription => string.Equals(apiParameterDescription.Name, openApiParameter_Concrete.Name, StringComparison.OrdinalIgnoreCase));

                string? mapping = apiParameterDescription?.Type.EnumMapping();
                if (mapping is null)
                {
                    continue;
                }

                string text = $"Accepts the member name or its integer value. Send the integer: it never changes, while member names have been renamed before. Values: {mapping}.";
                if (openApiParameter_Concrete.Description?.Contains(text, StringComparison.Ordinal) == true)
                {
                    continue;
                }

                openApiParameter_Concrete.Description = string.IsNullOrWhiteSpace(openApiParameter_Concrete.Description) ? text : $"{openApiParameter_Concrete.Description}\n\n{text}";
            }
        }
    }
}
