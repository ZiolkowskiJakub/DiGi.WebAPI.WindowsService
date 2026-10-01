#### [DiGi\.WebAPI\.WindowsService](DiGi.WebAPI.WindowsService.Overview.md 'DiGi\.WebAPI\.WindowsService\.Overview')

## DiGi\.WebAPI\.WindowsService\.Modify Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Modify.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.CamelCasePropertyNames(thisMicrosoft.OpenApi.OpenApiSchema)'></a>

## Modify\.CamelCasePropertyNames\(this OpenApiSchema\) Method

Renames the properties of a schema, and its `required` entries, to camelCase \- the names the host's MVC JSON options write \([ConfigureJsonSerializerOptions\(this JsonSerializerOptions\)](DiGi.WebAPI.WindowsService.Modify.md#DiGi.WebAPI.WindowsService.Modify.Modify.ConfigureJsonSerializerOptions(thisSystem.Text.Json.JsonSerializerOptions) 'DiGi\.WebAPI\.WindowsService\.Modify\.Modify\.ConfigureJsonSerializerOptions\(this System\.Text\.Json\.JsonSerializerOptions\)')\)\.

Needed for payloads written by the MVC formatter only: Swashbuckle documents a `[JsonPropertyName]` as spelled, while the host's `ForceCamelCaseModifier` writes every MVC member camelCase regardless. Never apply it to a DiGi `ISerializableObject` payload, which the DiGi serializer writes under its exact names.

```csharp
public static void CamelCasePropertyNames(this Microsoft.OpenApi.OpenApiSchema? openApiSchema);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.CamelCasePropertyNames(thisMicrosoft.OpenApi.OpenApiSchema).openApiSchema'></a>

`openApiSchema` [Microsoft\.OpenApi\.OpenApiSchema](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapischema 'Microsoft\.OpenApi\.OpenApiSchema')

The schema to rename the properties of\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.ConfigureJsonSerializerOptions(thisSystem.Text.Json.JsonSerializerOptions)'></a>

## Modify\.ConfigureJsonSerializerOptions\(this JsonSerializerOptions\) Method

Applies the host's MVC JSON conventions: camelCase property names \(including members carrying a
[System\.Text\.Json\.Serialization\.JsonPropertyNameAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute 'System\.Text\.Json\.Serialization\.JsonPropertyNameAttribute')\), enums written as their member names, and `null` values omitted\.

These options govern only payloads written through the MVC formatter (`Ok(...)` POCOs, `ProblemDetails`); DiGi `ISerializableObject` payloads are written by the DiGi serializer and do not follow them.

```csharp
public static void ConfigureJsonSerializerOptions(this System.Text.Json.JsonSerializerOptions? jsonSerializerOptions);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.ConfigureJsonSerializerOptions(thisSystem.Text.Json.JsonSerializerOptions).jsonSerializerOptions'></a>

`jsonSerializerOptions` [System\.Text\.Json\.JsonSerializerOptions](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions 'System\.Text\.Json\.JsonSerializerOptions')

The MVC JSON serializer options to configure\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.ConfigureSchemaGeneration(thisSwashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions)'></a>

## Modify\.ConfigureSchemaGeneration\(this SwaggerGenOptions\) Method

Applies the host's schema and parameter naming conventions to the Swagger generator: camelCase query parameters,
the schema filter that shapes every payload schema, the integer values on the description of every enum
parameter, and the removal of the enum components that only DiGi payloads used \(ZiolkowskiJakub/DiGi\.WebAPI\.WindowsService\#6\)\.

Kept apart from the document registration in `Program` so that tests generate schemas through exactly the configuration the host serves.

```csharp
public static void ConfigureSchemaGeneration(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions? swaggerGenOptions);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.ConfigureSchemaGeneration(thisSwashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions).swaggerGenOptions'></a>

`swaggerGenOptions` [Swashbuckle\.AspNetCore\.SwaggerGen\.SwaggerGenOptions](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.swaggergenoptions 'Swashbuckle\.AspNetCore\.SwaggerGen\.SwaggerGenOptions')

The Swagger generator options to configure\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.IncludeAssemblyXmlComments(thisSwashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions,System.Collections.Generic.IEnumerable_System.Reflection.Assembly_)'></a>

## Modify\.IncludeAssemblyXmlComments\(this SwaggerGenOptions, IEnumerable\<Assembly\>\) Method

Attaches the XML documentation file found beside each of the given assemblies to the Swagger generator, so that
operations, parameters and schemas carry the `<summary>` of the code they describe\.

Dynamic assemblies and assemblies without a location are skipped, as are assemblies without a sibling `.xml` file; a file that fails to load is logged and skipped.

```csharp
public static void IncludeAssemblyXmlComments(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions? swaggerGenOptions, System.Collections.Generic.IEnumerable<System.Reflection.Assembly>? assemblies);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.IncludeAssemblyXmlComments(thisSwashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions,System.Collections.Generic.IEnumerable_System.Reflection.Assembly_).swaggerGenOptions'></a>

`swaggerGenOptions` [Swashbuckle\.AspNetCore\.SwaggerGen\.SwaggerGenOptions](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.swaggergenoptions 'Swashbuckle\.AspNetCore\.SwaggerGen\.SwaggerGenOptions')

The Swagger generator options to configure\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.IncludeAssemblyXmlComments(thisSwashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions,System.Collections.Generic.IEnumerable_System.Reflection.Assembly_).assemblies'></a>

`assemblies` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Reflection\.Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly 'System\.Reflection\.Assembly')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The assemblies whose documentation is attached\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.InitializeAsync(thisSystem.Reflection.Assembly,Microsoft.Extensions.DependencyInjection.IServiceCollection)'></a>

## Modify\.InitializeAsync\(this Assembly, IServiceCollection\) Method

Initializes the specified assembly by scanning for classes inheriting from [DiGi\.WebAPI\.Classes\.WebAPIController](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.webapicontroller 'DiGi\.WebAPI\.Classes\.WebAPIController') 
and executing any static initialization methods named "Initialize" or "InitializeAsync" within a class named "Modify"\.

```csharp
public static System.Threading.Tasks.Task<bool> InitializeAsync(this System.Reflection.Assembly? assembly, Microsoft.Extensions.DependencyInjection.IServiceCollection? serviceCollection);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.InitializeAsync(thisSystem.Reflection.Assembly,Microsoft.Extensions.DependencyInjection.IServiceCollection).assembly'></a>

`assembly` [System\.Reflection\.Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly 'System\.Reflection\.Assembly')

The assembly to be scanned for controllers and initialization logic\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.InitializeAsync(thisSystem.Reflection.Assembly,Microsoft.Extensions.DependencyInjection.IServiceCollection).serviceCollection'></a>

`serviceCollection` [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection')

The [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection') used to register services and controllers\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if 
            controllers were registered or initialization methods were successfully executed; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.RemoveUnreferencedEnumSchemas(thisMicrosoft.OpenApi.OpenApiDocument)'></a>

## Modify\.RemoveUnreferencedEnumSchemas\(this OpenApiDocument\) Method

Removes every enum component that nothing in the document references\.

A DiGi payload member declares its enum inline ([OpenApiSchema\(this Type, string, bool\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Create.OpenApiSchema(thisSystem.Type,string,bool) 'DiGi\.WebAPI\.WindowsService\.Create\.OpenApiSchema\(this System\.Type, string, bool\)')), but generating the member's schema still registers the shared component, which lists member names. An enum used only by DiGi payloads would therefore stay in the document unreferenced, advertising the string form its payloads do not carry; an enum referenced by a query parameter or an MVC payload is kept. Only enum components are removed - an unreferenced object component is left alone. See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6.

The host registers it in [ConfigureSchemaGeneration\(this SwaggerGenOptions\)](DiGi.WebAPI.WindowsService.Modify.md#DiGi.WebAPI.WindowsService.Modify.Modify.ConfigureSchemaGeneration(thisSwashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions) 'DiGi\.WebAPI\.WindowsService\.Modify\.Modify\.ConfigureSchemaGeneration\(this Swashbuckle\.AspNetCore\.SwaggerGen\.SwaggerGenOptions\)'), ahead of any `IWebAPIDocumentFilter` an extension brings: such a filter adding a reference to an enum component would find it removed. No loaded extension has one (2026-10-01).

```csharp
public static void RemoveUnreferencedEnumSchemas(this Microsoft.OpenApi.OpenApiDocument? openApiDocument);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.RemoveUnreferencedEnumSchemas(thisMicrosoft.OpenApi.OpenApiDocument).openApiDocument'></a>

`openApiDocument` [Microsoft\.OpenApi\.OpenApiDocument](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapidocument 'Microsoft\.OpenApi\.OpenApiDocument')

The document to clean up\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.UpdateEnumParameterDescriptions(thisMicrosoft.OpenApi.OpenApiOperation,Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription)'></a>

## Modify\.UpdateEnumParameterDescriptions\(this OpenApiOperation, ApiDescription\) Method

Appends the integer values of the enum to the description of every operation parameter typed by an enum, and advises sending the integer\.

The parameter keeps its schema - a reference to the shared component listing the member names - because that is accurate for binding: ASP.NET binds the member name and the integer alike. The integer is the stable half of that contract (`Coding - WebAPI Contracts.md`, "Send enum values as integers"): member names have been renamed (`Subdivison` to `Subdivision`, after which the old spelling is a 400) while the integers never moved. See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6.

Works on the finished operation rather than per parameter because a parameter's own XML description is written by a parameter filter registered after the host's, which would overwrite text appended earlier.

```csharp
public static void UpdateEnumParameterDescriptions(this Microsoft.OpenApi.OpenApiOperation? openApiOperation, Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription? apiDescription);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.UpdateEnumParameterDescriptions(thisMicrosoft.OpenApi.OpenApiOperation,Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription).openApiOperation'></a>

`openApiOperation` [Microsoft\.OpenApi\.OpenApiOperation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapioperation 'Microsoft\.OpenApi\.OpenApiOperation')

The operation whose parameters are described\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.UpdateEnumParameterDescriptions(thisMicrosoft.OpenApi.OpenApiOperation,Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription).apiDescription'></a>

`apiDescription` [Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.ApiDescription](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apiexplorer.apidescription 'Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.ApiDescription')

The API description of the operation, giving the CLR type of each parameter\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.UpdateSerializableObjectProperties(thisMicrosoft.OpenApi.OpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext)'></a>

## Modify\.UpdateSerializableObjectProperties\(this OpenApiSchema, SchemaFilterContext\) Method

Rewrites the schema of a DiGi `ISerializableObject` payload type so that it describes what the DiGi serializer writes, not what the MVC JSON options would\.

Always a JSON object - also for a DiGi type that is an `IEnumerable` (a `Weather` is an `IEnumerable<WeatherRecord>`), which Swashbuckle on its own documents as an array.

For a type whose JSON is its member contract ([HasMemberWireFormat\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.HasMemberWireFormat(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.HasMemberWireFormat\(this System\.Type\)')): one property per written member ([WireMembers\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.WireMembers(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.WireMembers\(this System\.Type\)')), under its exact JSON name, all of them required because the serializer writes every member - `null` explicitly. Member schemas are generated against the public property the member is named after where there is one, so field-backed types get that property's XML description; `readOnly` is cleared because the serializer reads members back regardless of setters, and a nullable member typed by a schema component is wrapped so that `null` validates against it. The schema is closed with `additionalProperties: false` unless a loaded type derives from it ([DerivedTypes\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.DerivedTypes(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.DerivedTypes\(this System\.Type\)')): a member declared as the type may then hold the subclass, which is written with its extra members.

For any other DiGi type (an interface, an abstract type, a type writing its own JSON): an open schema requiring only `_type`, the discriminator naming the concrete type whose members follow.

An enum member - also the element of a collection or the value of a dictionary - is written as its underlying integer, so it is declared by an inline integer schema ([OpenApiSchema\(this Type, string, bool\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Create.OpenApiSchema(thisSystem.Type,string,bool) 'DiGi\.WebAPI\.WindowsService\.Create\.OpenApiSchema\(this System\.Type, string, bool\)')) carrying the member's description, the enum's and the name to integer mapping, instead of a reference to the shared enum component, which keeps listing the member names that query parameters bind (ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6).

```csharp
public static void UpdateSerializableObjectProperties(this Microsoft.OpenApi.OpenApiSchema? openApiSchema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext? schemaFilterContext);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.UpdateSerializableObjectProperties(thisMicrosoft.OpenApi.OpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext).openApiSchema'></a>

`openApiSchema` [Microsoft\.OpenApi\.OpenApiSchema](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapischema 'Microsoft\.OpenApi\.OpenApiSchema')

The schema generated for the type: its component, or the inline schema of a type Swashbuckle gives no component\.

<a name='DiGi.WebAPI.WindowsService.Modify.Modify.UpdateSerializableObjectProperties(thisMicrosoft.OpenApi.OpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext).schemaFilterContext'></a>

`schemaFilterContext` [Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.schemafiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext')

The schema filter context naming the type, with the generator and repository used for member schemas\.

<a name='DiGi.WebAPI.WindowsService.Modify.ProblemDetailsResponseOperationFilter'></a>

## ProblemDetailsResponseOperationFilter Class

Ensures that all 4xx and 5xx responses use the ProblemDetails schema\.

```csharp
public class ProblemDetailsResponseOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → ProblemDetailsResponseOperationFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Modify.ProblemDetailsResponseOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext)'></a>

## ProblemDetailsResponseOperationFilter\.Apply\(OpenApiOperation, OperationFilterContext\) Method

Points the `application/json` content of every 4xx and 5xx response that has content at the `ProblemDetails` component\.

```csharp
public void Apply(Microsoft.OpenApi.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.ProblemDetailsResponseOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).operation'></a>

`operation` [Microsoft\.OpenApi\.OpenApiOperation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapioperation 'Microsoft\.OpenApi\.OpenApiOperation')

The OpenAPI operation to be modified\.

<a name='DiGi.WebAPI.WindowsService.Modify.ProblemDetailsResponseOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.operationfiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext')

The context carrying the document the schema reference resolves against\.

Implements [Apply\(OpenApiOperation, OperationFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter.apply#swashbuckle-aspnetcore-swaggergen-ioperationfilter-apply(microsoft-openapi-openapioperation-swashbuckle-aspnetcore-swaggergen-operationfiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter\.Apply\(Microsoft\.OpenApi\.OpenApiOperation,Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext\)')

<a name='DiGi.WebAPI.WindowsService.Modify.SecurityRequirementOperationFilter'></a>

## SecurityRequirementOperationFilter Class

Adds an API key security requirement to operations that declare a 401 response\.

```csharp
public class SecurityRequirementOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → SecurityRequirementOperationFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Modify.SecurityRequirementOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext)'></a>

## SecurityRequirementOperationFilter\.Apply\(OpenApiOperation, OperationFilterContext\) Method

Attaches the `apiKey` security requirement to the operation when it declares a 401 response\.

```csharp
public void Apply(Microsoft.OpenApi.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.SecurityRequirementOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).operation'></a>

`operation` [Microsoft\.OpenApi\.OpenApiOperation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapioperation 'Microsoft\.OpenApi\.OpenApiOperation')

The OpenAPI operation to be modified\.

<a name='DiGi.WebAPI.WindowsService.Modify.SecurityRequirementOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.operationfiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext')

The context carrying the document the security scheme reference resolves against\.

Implements [Apply\(OpenApiOperation, OperationFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter.apply#swashbuckle-aspnetcore-swaggergen-ioperationfilter-apply(microsoft-openapi-openapioperation-swashbuckle-aspnetcore-swaggergen-operationfiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter\.Apply\(Microsoft\.OpenApi\.OpenApiOperation,Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext\)')