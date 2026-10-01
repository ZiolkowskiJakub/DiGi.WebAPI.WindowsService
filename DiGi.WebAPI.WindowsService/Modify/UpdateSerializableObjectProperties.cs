using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;

namespace DiGi.WebAPI.WindowsService.Modify
{
    public static partial class Modify
    {
        /// <summary>
        /// Rewrites the schema of a DiGi <c>ISerializableObject</c> payload type so that it describes what the DiGi serializer writes, not what the MVC JSON options would.
        /// <para>Always a JSON object - also for a DiGi type that is an <c>IEnumerable</c> (a <c>Weather</c> is an <c>IEnumerable&lt;WeatherRecord&gt;</c>), which Swashbuckle on its own documents as an array.</para>
        /// <para>For a type whose JSON is its member contract (<see cref="Query.HasMemberWireFormat"/>): one property per written member (<see cref="Query.WireMembers"/>), under its exact JSON name, all of them required because the serializer writes every member - <c>null</c> explicitly. Member schemas are generated against the public property the member is named after where there is one, so field-backed types get that property's XML description; <c>readOnly</c> is cleared because the serializer reads members back regardless of setters, and a nullable member typed by a schema component is wrapped so that <c>null</c> validates against it. The schema is closed with <c>additionalProperties: false</c> unless a loaded type derives from it (<see cref="Query.DerivedTypes"/>): a member declared as the type may then hold the subclass, which is written with its extra members.</para>
        /// <para>For any other DiGi type (an interface, an abstract type, a type writing its own JSON): an open schema requiring only <c>_type</c>, the discriminator naming the concrete type whose members follow.</para>
        /// <para>An enum member - also the element of a collection or the value of a dictionary - is written as its underlying integer, so it is declared by an inline integer schema (<see cref="Create.OpenApiSchema"/>) carrying the member's description, the enum's and the name to integer mapping, instead of a reference to the shared enum component, which keeps listing the member names that query parameters bind (ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6).</para>
        /// </summary>
        /// <param name="openApiSchema">The schema generated for the type: its component, or the inline schema of a type Swashbuckle gives no component.</param>
        /// <param name="schemaFilterContext">The schema filter context naming the type, with the generator and repository used for member schemas.</param>
        public static void UpdateSerializableObjectProperties(this OpenApiSchema? openApiSchema, SchemaFilterContext? schemaFilterContext)
        {
            if (openApiSchema is null || schemaFilterContext?.Type is not Type type)
            {
                return;
            }

            const string name_Type = Core.Constants.Serialization.PropertyName.Type;

            // The DiGi serializer writes every ISerializableObject as a JSON object; keep only the null flag Swashbuckle set.
            openApiSchema.Type = JsonSchemaType.Object | (openApiSchema.Type.GetValueOrDefault() & JsonSchemaType.Null);
            openApiSchema.Items = null;

            if (!type.HasMemberWireFormat())
            {
                openApiSchema.Properties = new Dictionary<string, IOpenApiSchema>
                {
                    [name_Type] = TypeSchema(null, false)
                };
                openApiSchema.Required = new HashSet<string> { name_Type };
                openApiSchema.AdditionalPropertiesAllowed = true;
                openApiSchema.AdditionalProperties = null;
                return;
            }

            bool derived = type.DerivedTypes().Count != 0;

            Dictionary<string, IOpenApiSchema> properties = [];
            HashSet<string> required = [];
            foreach ((string name, MemberInfo memberInfo) in type.WireMembers())
            {
                if (name == name_Type)
                {
                    properties[name] = TypeSchema(Core.Query.FullTypeName(type), derived);
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

                // The serializer writes an enum as its underlying integer, while the shared component lists member names for
                // the query parameters that bind them: declare the member's enum inline (#6).
                OpenApiSchema? openApiSchema_Enum = EnumSchema(openApiSchema_Member, type_Member, nullable, openApiSchema_Member.Description, schemaFilterContext.SchemaRepository);
                if (openApiSchema_Enum is not null)
                {
                    properties[name] = openApiSchema_Enum;
                    required.Add(name);
                    continue;
                }

                if (openApiSchema_Member is OpenApiSchema openApiSchema_Member_Concrete)
                {
                    openApiSchema_Member_Concrete.ReadOnly = false;

                    // A collection or dictionary of enums: its elements are written as integers too.
                    if (EnumSchema(openApiSchema_Member_Concrete.Items, ElementType(type_Member, false), null, null, schemaFilterContext.SchemaRepository) is OpenApiSchema openApiSchema_Item)
                    {
                        openApiSchema_Member_Concrete.Items = openApiSchema_Item;
                    }

                    if (EnumSchema(openApiSchema_Member_Concrete.AdditionalProperties, ElementType(type_Member, true), null, null, schemaFilterContext.SchemaRepository) is OpenApiSchema openApiSchema_Value)
                    {
                        openApiSchema_Member_Concrete.AdditionalProperties = openApiSchema_Value;
                    }

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
            openApiSchema.AdditionalPropertiesAllowed = derived;
            openApiSchema.AdditionalProperties = null;

            // The inline integer schema replacing a reference to an enum component, described by the given description (the
            // member's) followed by the component's (the enum's); null when the schema is no such reference. Nullability
            // defaults to the type's own: a nullable enum element admits null, an enum element does not.
            static OpenApiSchema? EnumSchema(IOpenApiSchema? openApiSchema, Type? type, bool? nullable, string? description, SchemaRepository schemaRepository)
            {
                if (openApiSchema is not OpenApiSchemaReference openApiSchemaReference || type is null)
                {
                    return null;
                }

                if (!(Nullable.GetUnderlyingType(type) ?? type).IsEnum)
                {
                    return null;
                }

                List<string> descriptions = [];
                if (!string.IsNullOrWhiteSpace(description))
                {
                    descriptions.Add(description);
                }

                if (openApiSchemaReference.Reference.Id is string id && schemaRepository.Schemas.TryGetValue(id, out IOpenApiSchema? openApiSchema_Component) && !string.IsNullOrWhiteSpace(openApiSchema_Component.Description))
                {
                    descriptions.Add(openApiSchema_Component.Description);
                }

                return Create.OpenApiSchema(type, descriptions.Count == 0 ? null : string.Join("\n\n", descriptions), nullable ?? false);
            }

            // The element type of a collection, or the value type of a dictionary keyed by anything; null when the type is
            // neither.
            static Type? ElementType(Type type, bool dictionary)
            {
                if (!dictionary && type.IsArray)
                {
                    return type.GetElementType();
                }

                Type[] types = type.IsInterface ? [type, .. type.GetInterfaces()] : type.GetInterfaces();
                foreach (Type type_Interface in types)
                {
                    if (!type_Interface.IsGenericType)
                    {
                        continue;
                    }

                    Type type_Definition = type_Interface.GetGenericTypeDefinition();
                    if (dictionary && (type_Definition == typeof(IDictionary<,>) || type_Definition == typeof(IReadOnlyDictionary<,>)))
                    {
                        return type_Interface.GetGenericArguments()[1];
                    }

                    if (!dictionary && type_Definition == typeof(IEnumerable<>))
                    {
                        return type_Interface.GetGenericArguments()[0];
                    }
                }

                return null;
            }

            static OpenApiSchema TypeSchema(string? fullTypeName, bool derived)
            {
                string description = "Type discriminator written on every DiGi payload: the full name of the serialized type and the short name of its assembly, `Namespace.Type,ShortAssembly` (no version, culture or key).";
                if (fullTypeName is null)
                {
                    description += " This type is an interface, an abstract type or writes its own JSON, so its members are not listed here: the payload carries the members of the concrete type named by `_type`.";
                }
                else if (derived)
                {
                    description += " Other types derive from this one, so `_type` may name one of them instead: the payload then carries that type's additional members too.";
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
