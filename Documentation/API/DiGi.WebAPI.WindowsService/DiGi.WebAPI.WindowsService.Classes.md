#### [DiGi\.WebAPI\.WindowsService](DiGi.WebAPI.WindowsService.Overview.md 'DiGi\.WebAPI\.WindowsService\.Overview')

## DiGi\.WebAPI\.WindowsService\.Classes Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Classes.EnumParameterDescriptionFilter'></a>

## EnumParameterDescriptionFilter Class

Describes every enum\-typed operation parameter with the integer values of its enum \([UpdateEnumParameterDescriptions\(this OpenApiOperation, ApiDescription\)](DiGi.WebAPI.WindowsService.Modify.md#DiGi.WebAPI.WindowsService.Modify.Modify.UpdateEnumParameterDescriptions(thisMicrosoft.OpenApi.OpenApiOperation,Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription) 'DiGi\.WebAPI\.WindowsService\.Modify\.Modify\.UpdateEnumParameterDescriptions\(this Microsoft\.OpenApi\.OpenApiOperation, Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.ApiDescription\)')\)\.

An operation filter rather than a parameter filter: operation filters run once all parameters are built, so the text survives the XML comment parameter filter, which is registered after the host's own filters and overwrites a parameter's description.

```csharp
public class EnumParameterDescriptionFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → EnumParameterDescriptionFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Classes.EnumParameterDescriptionFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext)'></a>

## EnumParameterDescriptionFilter\.Apply\(OpenApiOperation, OperationFilterContext\) Method

Appends the integer values of its enum to the description of each enum\-typed parameter of the operation\.

```csharp
public void Apply(Microsoft.OpenApi.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Classes.EnumParameterDescriptionFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).operation'></a>

`operation` [Microsoft\.OpenApi\.OpenApiOperation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapioperation 'Microsoft\.OpenApi\.OpenApiOperation')

The OpenAPI operation to be modified\.

<a name='DiGi.WebAPI.WindowsService.Classes.EnumParameterDescriptionFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.operationfiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext')

The context containing the API description of the operation\.

Implements [Apply\(OpenApiOperation, OperationFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter.apply#swashbuckle-aspnetcore-swaggergen-ioperationfilter-apply(microsoft-openapi-openapioperation-swashbuckle-aspnetcore-swaggergen-operationfiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter\.Apply\(Microsoft\.OpenApi\.OpenApiOperation,Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext\)')

<a name='DiGi.WebAPI.WindowsService.Classes.SchemaReferenceVisitor'></a>

## SchemaReferenceVisitor Class

Collects the id of every schema component referenced in a walked OpenAPI document \- from operations, parameters, request bodies, responses and from inside other components \(`properties`, `items`, `allOf`, \.\.\.\)\.

Overrides `Visit(IOpenApiReferenceHolder)`: the walker reports a `$ref` there, while `Visit(IOpenApiSchema)` never sees one (measured with Microsoft.OpenApi 2.7.5, 2026-10-01).

```csharp
public class SchemaReferenceVisitor : Microsoft.OpenApi.OpenApiVisitorBase
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [Microsoft\.OpenApi\.OpenApiVisitorBase](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapivisitorbase 'Microsoft\.OpenApi\.OpenApiVisitorBase') → SchemaReferenceVisitor
### Properties

<a name='DiGi.WebAPI.WindowsService.Classes.SchemaReferenceVisitor.Ids'></a>

## SchemaReferenceVisitor\.Ids Property

Gets the ids of the referenced schema components collected so far\.

```csharp
public System.Collections.Generic.HashSet<string> Ids { get; }
```

#### Property Value
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')
### Methods

<a name='DiGi.WebAPI.WindowsService.Classes.SchemaReferenceVisitor.Visit(Microsoft.OpenApi.IOpenApiReferenceHolder)'></a>

## SchemaReferenceVisitor\.Visit\(IOpenApiReferenceHolder\) Method

Records the component id of a schema reference\.

```csharp
public override void Visit(Microsoft.OpenApi.IOpenApiReferenceHolder referenceHolder);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Classes.SchemaReferenceVisitor.Visit(Microsoft.OpenApi.IOpenApiReferenceHolder).referenceHolder'></a>

`referenceHolder` [Microsoft\.OpenApi\.IOpenApiReferenceHolder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.iopenapireferenceholder 'Microsoft\.OpenApi\.IOpenApiReferenceHolder')

The reference being visited\.

<a name='DiGi.WebAPI.WindowsService.Classes.UnreferencedEnumDocumentFilter'></a>

## UnreferencedEnumDocumentFilter Class

Removes the enum components a generated document no longer references \([RemoveUnreferencedEnumSchemas\(this OpenApiDocument\)](DiGi.WebAPI.WindowsService.Modify.md#DiGi.WebAPI.WindowsService.Modify.Modify.RemoveUnreferencedEnumSchemas(thisMicrosoft.OpenApi.OpenApiDocument) 'DiGi\.WebAPI\.WindowsService\.Modify\.Modify\.RemoveUnreferencedEnumSchemas\(this Microsoft\.OpenApi\.OpenApiDocument\)')\): those whose only users were DiGi payload members, which declare their enum inline\.

```csharp
public class UnreferencedEnumDocumentFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → UnreferencedEnumDocumentFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.IDocumentFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.idocumentfilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.IDocumentFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Classes.UnreferencedEnumDocumentFilter.Apply(Microsoft.OpenApi.OpenApiDocument,Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext)'></a>

## UnreferencedEnumDocumentFilter\.Apply\(OpenApiDocument, DocumentFilterContext\) Method

Removes every enum component that nothing in the document references\.

```csharp
public void Apply(Microsoft.OpenApi.OpenApiDocument swaggerDoc, Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Classes.UnreferencedEnumDocumentFilter.Apply(Microsoft.OpenApi.OpenApiDocument,Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext).swaggerDoc'></a>

`swaggerDoc` [Microsoft\.OpenApi\.OpenApiDocument](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapidocument 'Microsoft\.OpenApi\.OpenApiDocument')

The OpenAPI document to be modified\.

<a name='DiGi.WebAPI.WindowsService.Classes.UnreferencedEnumDocumentFilter.Apply(Microsoft.OpenApi.OpenApiDocument,Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.DocumentFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.documentfiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.DocumentFilterContext')

The context of the document being generated\.

Implements [Apply\(OpenApiDocument, DocumentFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.idocumentfilter.apply#swashbuckle-aspnetcore-swaggergen-idocumentfilter-apply(microsoft-openapi-openapidocument-swashbuckle-aspnetcore-swaggergen-documentfiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.IDocumentFilter\.Apply\(Microsoft\.OpenApi\.OpenApiDocument,Swashbuckle\.AspNetCore\.SwaggerGen\.DocumentFilterContext\)')

<a name='DiGi.WebAPI.WindowsService.Classes.WireFormatSchemaFilter'></a>

## WireFormatSchemaFilter Class

Makes every payload schema describe the format actually written on the wire, which depends on who writes the payload\.

A DiGi `ISerializableObject` is written by the DiGi serializer - exact member names (PascalCase by convention), a mandatory `_type` discriminator, every member present - and its schema is rebuilt from that serializer's member contract ([UpdateSerializableObjectProperties\(this OpenApiSchema, SchemaFilterContext\)](DiGi.WebAPI.WindowsService.Modify.md#DiGi.WebAPI.WindowsService.Modify.Modify.UpdateSerializableObjectProperties(thisMicrosoft.OpenApi.OpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext) 'DiGi\.WebAPI\.WindowsService\.Modify\.Modify\.UpdateSerializableObjectProperties\(this Microsoft\.OpenApi\.OpenApiSchema, Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext\)')). Anything else (`Ok(...)` POCOs, `ProblemDetails`) is written by the MVC formatter in camelCase, and its schema is renamed to match ([CamelCasePropertyNames\(this OpenApiSchema\)](DiGi.WebAPI.WindowsService.Modify.md#DiGi.WebAPI.WindowsService.Modify.Modify.CamelCasePropertyNames(thisMicrosoft.OpenApi.OpenApiSchema) 'DiGi\.WebAPI\.WindowsService\.Modify\.Modify\.CamelCasePropertyNames\(this Microsoft\.OpenApi\.OpenApiSchema\)')).

See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#3. Enum members of a DiGi payload travel as integers and are declared inline (#6); the shared enum components stay the member-name strings that query parameters and MVC payloads use.

```csharp
public class WireFormatSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → WireFormatSchemaFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.ISchemaFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ischemafilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.ISchemaFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Classes.WireFormatSchemaFilter.Apply(Microsoft.OpenApi.IOpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext)'></a>

## WireFormatSchemaFilter\.Apply\(IOpenApiSchema, SchemaFilterContext\) Method

Rewrites the schema for the wire format of its type: the DiGi member contract for an `ISerializableObject` component, camelCase names for anything else\.

```csharp
public void Apply(Microsoft.OpenApi.IOpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Classes.WireFormatSchemaFilter.Apply(Microsoft.OpenApi.IOpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext).schema'></a>

`schema` [Microsoft\.OpenApi\.IOpenApiSchema](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.iopenapischema 'Microsoft\.OpenApi\.IOpenApiSchema')

The OpenAPI schema to be modified\.

<a name='DiGi.WebAPI.WindowsService.Classes.WireFormatSchemaFilter.Apply(Microsoft.OpenApi.IOpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.schemafiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext')

The context containing information about the schema being filtered\.

Implements [Apply\(IOpenApiSchema, SchemaFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ischemafilter.apply#swashbuckle-aspnetcore-swaggergen-ischemafilter-apply(microsoft-openapi-iopenapischema-swashbuckle-aspnetcore-swaggergen-schemafiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.ISchemaFilter\.Apply\(Microsoft\.OpenApi\.IOpenApiSchema,Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext\)')