using DiGi.Core.Interfaces;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DiGi.WebAPI.WindowsService.Classes
{
    /// <summary>
    /// Makes every payload schema describe the format actually written on the wire, which depends on who writes the payload.
    /// <para>A DiGi <c>ISerializableObject</c> is written by the DiGi serializer - exact member names (PascalCase by convention), a mandatory <c>_type</c> discriminator, every member present - and its schema is rebuilt from that serializer's member contract (<see cref="Modify.UpdateSerializableObjectProperties"/>). Anything else (<c>Ok(...)</c> POCOs, <c>ProblemDetails</c>) is written by the MVC formatter in camelCase, and its schema is renamed to match (<see cref="Modify.CamelCasePropertyNames"/>).</para>
    /// <para>See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#3; enum declaration is #6.</para>
    /// </summary>
    public class WireFormatSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Rewrites the schema for the wire format of its type: the DiGi member contract for an <c>ISerializableObject</c> component, camelCase names for anything else.
        /// </summary>
        /// <param name="schema">The OpenAPI schema to be modified.</param>
        /// <param name="context">The context containing information about the schema being filtered.</param>
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is not OpenApiSchema openApiSchema || context?.Type is null)
            {
                return;
            }

            if (typeof(ISerializableObject).IsAssignableFrom(context.Type))
            {
                // Only the component definition is rebuilt; a member typed by a DiGi type references it.
                if (context.MemberInfo is null && context.ParameterInfo is null)
                {
                    openApiSchema.UpdateSerializableObjectProperties(context);
                }

                return;
            }

            openApiSchema.CamelCasePropertyNames();
        }
    }
}
