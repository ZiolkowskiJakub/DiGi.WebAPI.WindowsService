using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using DiGi.WebAPI.WindowsService.Modify;

namespace DiGi.WebAPI.WindowsService.Classes
{
    /// <summary>
    /// Describes every enum-typed operation parameter with the integer values of its enum (<see cref="Modify.Modify.UpdateEnumParameterDescriptions"/>).
    /// <para>An operation filter rather than a parameter filter: operation filters run once all parameters are built, so the text survives the XML comment parameter filter, which is registered after the host's own filters and overwrites a parameter's description.</para>
    /// </summary>
    public class EnumParameterDescriptionFilter : IOperationFilter
    {
        /// <summary>
        /// Appends the integer values of its enum to the description of each enum-typed parameter of the operation.
        /// </summary>
        /// <param name="operation">The OpenAPI operation to be modified.</param>
        /// <param name="context">The context containing the API description of the operation.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.UpdateEnumParameterDescriptions(context?.ApiDescription);
        }
    }
}
