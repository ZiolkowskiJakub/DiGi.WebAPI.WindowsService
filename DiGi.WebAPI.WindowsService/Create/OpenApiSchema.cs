using Microsoft.OpenApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Nodes;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Create
    {
        /// <summary>
        /// Creates the inline schema of an enum as the DiGi serializer writes it: an integer, the member's underlying value.
        /// <para>The values are listed in numeric order (<see cref="Query.EnumMembers"/>), named for code generators in the same order by <c>x-enum-varnames</c> (openapi-generator) and <c>x-enumNames</c> (NSwag), and mapped to the member names in the description. The format is <c>int64</c> for a <c>long</c>, <c>uint</c> or <c>ulong</c> enum, <c>int32</c> otherwise, and left out when a <c>ulong</c> value exceeds <c>int64</c>.</para>
        /// <para>A nullable enum gets the <c>null</c> type and <c>null</c> last among the values: OpenAPI 3.0's <c>nullable</c> widens <c>type</c> only, so a value list without <c>null</c> would still reject the explicit <c>null</c> the serializer writes. A <c>[Flags]</c> enum lists no values - a combined value is in no member list - and describes its bits instead; an enum without members lists no values either, because an empty list would reject everything.</para>
        /// <para>See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6: the shared enum component stays the string schema query parameters bind against, so a DiGi payload member declares its enum inline.</para>
        /// </summary>
        /// <param name="type">The enum type, or a nullable enum type (which makes the schema nullable).</param>
        /// <param name="description">The description to start with - the member's and the enum's; the wire mapping is appended to it.</param>
        /// <param name="nullable">Whether the value may be <c>null</c>, besides a nullable enum type.</param>
        /// <returns>The inline integer schema; <c>null</c> when the type is <c>null</c> or not an enum.</returns>
        public static OpenApiSchema? OpenApiSchema(this Type? type, string? description = null, bool nullable = false)
        {
            if (type is null)
            {
                return null;
            }

            Type? type_Nullable = Nullable.GetUnderlyingType(type);
            Type type_Enum = type_Nullable ?? type;
            if (!type_Enum.IsEnum)
            {
                return null;
            }

            nullable = nullable || type_Nullable is not null;

            List<(object Value, List<string> Names)> members = type_Enum.EnumMembers();
            bool flags = type_Enum.IsDefined(typeof(FlagsAttribute), false);

            OpenApiSchema openApiSchema = new()
            {
                Type = nullable ? JsonSchemaType.Integer | JsonSchemaType.Null : JsonSchemaType.Integer,
                Format = Type.GetTypeCode(Enum.GetUnderlyingType(type_Enum)) switch
                {
                    TypeCode.Int64 or TypeCode.UInt32 => "int64",
                    TypeCode.UInt64 => members.Any(member => (ulong)member.Value > long.MaxValue) ? null : "int64",
                    _ => "int32"
                }
            };

            if (!flags && members.Count != 0)
            {
                List<JsonNode> jsonNodes = [.. members.Select(member => (JsonNode)Value(member.Value))];
                if (nullable)
                {
                    // A null entry is how Microsoft.OpenApi writes the JSON null value, although Enum is annotated non-null.
                    jsonNodes.Add(null!);
                }

                openApiSchema.Enum = jsonNodes;

                // Two arrays, not one shared: a JsonNode has a single parent.
                openApiSchema.Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    ["x-enum-varnames"] = new JsonNodeExtension(Names()),
                    ["x-enumNames"] = new JsonNodeExtension(Names())
                };
            }

            List<string> descriptions = [];
            if (!string.IsNullOrWhiteSpace(description))
            {
                descriptions.Add(description);
            }

            string? mapping = type_Enum.EnumMapping();
            if (mapping is not null)
            {
                descriptions.Add(flags ? $"Wire value (integer): a bitwise combination of {mapping}." : $"Wire values (integer): {mapping}.");
            }

            openApiSchema.Description = descriptions.Count == 0 ? null : string.Join("\n\n", descriptions);

            return openApiSchema;

            // The first name of each value is the member's own; the others are aliases.
            JsonArray Names()
            {
                return [.. members.Select(member => (JsonNode?)JsonValue.Create(member.Names[0]))];
            }

            // Typed by the enum's underlying type, as the DiGi serializer writes it, so GetValue<int> reads an int enum back.
            static JsonValue Value(object value)
            {
                return value switch
                {
                    sbyte @sbyte => JsonValue.Create(@sbyte),
                    byte @byte => JsonValue.Create(@byte),
                    short @short => JsonValue.Create(@short),
                    ushort @ushort => JsonValue.Create(@ushort),
                    int @int => JsonValue.Create(@int),
                    uint @uint => JsonValue.Create(@uint),
                    long @long => JsonValue.Create(@long),
                    ulong @ulong => JsonValue.Create(@ulong),
                    _ => JsonValue.Create(System.Convert.ToInt64(value, CultureInfo.InvariantCulture))
                };
            }
        }
    }
}
