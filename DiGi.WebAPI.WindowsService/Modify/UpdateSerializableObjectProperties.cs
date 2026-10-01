using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Modify
    {
        /// <summary>
        /// Rewrites the schema of a DiGi <c>ISerializableObject</c> payload type so that it describes what the DiGi serializer writes, not what the MVC JSON options would.
        /// <para>For a type whose JSON is its member contract (<see cref="Query.IsClosedWireFormat"/>): one property per written member (<see cref="Query.WireMembers"/>), under its exact JSON name, all of them required because the serializer writes every member - <c>null</c> explicitly - and <c>additionalProperties: false</c>. Member schemas are generated against the public property the member is named after where there is one, so field-backed types get that property's XML description; <c>readOnly</c> is cleared because the serializer reads members back regardless of setters, and a nullable member typed by a schema component is wrapped so that <c>null</c> validates against it.</para>
        /// <para>For any other DiGi type (an interface, an abstract type, a type writing its own JSON): an open schema requiring only <c>_type</c>, the discriminator naming the concrete type whose members follow.</para>
        /// <para>Enum members keep the schema Swashbuckle generates for them; their wire form is ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6.</para>
        /// </summary>
        /// <param name="openApiSchema">The component schema generated for the type.</param>
        /// <param name="schemaFilterContext">The schema filter context naming the type, with the generator and repository used for member schemas.</param>
        public static void UpdateSerializableObjectProperties(this OpenApiSchema? openApiSchema, SchemaFilterContext? schemaFilterContext)
        {
            if (openApiSchema is null || schemaFilterContext?.Type is not Type type)
            {
                return;
            }

            const string name_Type = Core.Constants.Serialization.PropertyName.Type;

            if (!type.IsClosedWireFormat())
            {
                openApiSchema.Properties = new Dictionary<string, IOpenApiSchema>
                {
                    [name_Type] = TypeSchema(null)
                };
                openApiSchema.Required = new HashSet<string> { name_Type };
                openApiSchema.AdditionalPropertiesAllowed = true;
                openApiSchema.AdditionalProperties = null;
                return;
            }

            Dictionary<string, IOpenApiSchema> properties = [];
            HashSet<string> required = [];
            foreach ((string name, MemberInfo memberInfo) in type.WireMembers())
            {
                if (name == name_Type)
                {
                    properties[name] = TypeSchema(Core.Query.FullTypeName(type));
                    required.Add(name);
                    continue;
                }

                Type? type_Member = memberInfo switch
                {
                    PropertyInfo propertyInfo => propertyInfo.PropertyType,
                    FieldInfo fieldInfo => fieldInfo.FieldType,
                    _ => null
                };

                if (type_Member is null)
                {
                    continue;
                }

                // Field-backed members carry no XML documentation of their own: describe them through the public property
                // they are named after (JsonPropertyName(nameof(Property)) by convention).
                MemberInfo memberInfo_Schema = memberInfo;
                if (memberInfo is FieldInfo)
                {
                    PropertyInfo? propertyInfo_Public = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(propertyInfo => propertyInfo.Name == name);
                    if (propertyInfo_Public is not null)
                    {
                        memberInfo_Schema = propertyInfo_Public;
                    }
                }

                IOpenApiSchema openApiSchema_Member = schemaFilterContext.SchemaGenerator.GenerateSchema(type_Member, schemaFilterContext.SchemaRepository, memberInfo_Schema);

                // The serializer writes null exactly when the CLR type admits it. Swashbuckle sets this only on its own
                // property path, not for a member schema requested by MemberInfo (an int? came back non-nullable).
                bool nullable = !type_Member.IsValueType || Nullable.GetUnderlyingType(type_Member) is not null;

                if (openApiSchema_Member is OpenApiSchema openApiSchema_Member_Concrete)
                {
                    openApiSchema_Member_Concrete.ReadOnly = false;

                    // A schema without a type already admits null; flagging it would narrow it to null only.
                    if (nullable && openApiSchema_Member_Concrete.Type is JsonSchemaType jsonSchemaType)
                    {
                        openApiSchema_Member_Concrete.Type = jsonSchemaType | JsonSchemaType.Null;
                    }
                }
                else if (openApiSchema_Member is OpenApiSchemaReference && nullable)
                {
                    // Siblings of $ref are ignored in OpenAPI 3.0, so nullability needs a wrapper around the reference.
                    openApiSchema_Member = new OpenApiSchema
                    {
                        AllOf = [openApiSchema_Member],
                        Type = JsonSchemaType.Null
                    };
                }

                properties[name] = openApiSchema_Member;
                required.Add(name);
            }

            openApiSchema.Properties = properties;
            openApiSchema.Required = required;
            openApiSchema.AdditionalPropertiesAllowed = false;
            openApiSchema.AdditionalProperties = null;

            static OpenApiSchema TypeSchema(string? fullTypeName)
            {
                string description = "Type discriminator written on every DiGi payload: the full name of the serialized type and the short name of its assembly, `Namespace.Type,ShortAssembly` (no version, culture or key).";
                if (fullTypeName is null)
                {
                    description += " This type is an interface, an abstract type or writes its own JSON, so its members are not listed here: the payload carries the members of the concrete type named by `_type`.";
                }

                return new OpenApiSchema
                {
                    Type = JsonSchemaType.String,
                    Description = description,
                    Example = fullTypeName is null ? null : JsonValue.Create(fullTypeName)
                };
            }
        }
    }
}
