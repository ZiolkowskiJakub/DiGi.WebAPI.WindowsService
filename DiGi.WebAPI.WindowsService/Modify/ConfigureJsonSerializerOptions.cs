using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        /// <summary>
        /// Applies the host's MVC JSON conventions: camelCase property names (including members carrying a
        /// <see cref="JsonPropertyNameAttribute"/>), enums written as their member names, and <c>null</c> values omitted.
        /// <para>These options govern only payloads written through the MVC formatter (<c>Ok(...)</c> POCOs, <c>ProblemDetails</c>); DiGi <c>ISerializableObject</c> payloads are written by the DiGi serializer and do not follow them.</para>
        /// </summary>
        /// <param name="jsonSerializerOptions">The MVC JSON serializer options to configure.</param>
        public static void ConfigureJsonSerializerOptions(this JsonSerializerOptions? jsonSerializerOptions)
        {
            if (jsonSerializerOptions is null)
            {
                return;
            }

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { ForceCamelCaseModifier }
            };

            static void ForceCamelCaseModifier(JsonTypeInfo jsonTypeInfo)
            {
                if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
                {
                    return;
                }

                foreach (JsonPropertyInfo jsonPropertyInfo in jsonTypeInfo.Properties)
                {
                    if (jsonPropertyInfo.AttributeProvider is MemberInfo memberInfo)
                    {
                        jsonPropertyInfo.Name = JsonNamingPolicy.CamelCase.ConvertName(memberInfo.Name);
                    }
                }
            }
        }
    }
}
