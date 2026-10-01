#### [DiGi\.WebAPI\.WindowsService](DiGi.WebAPI.WindowsService.Overview.md 'DiGi\.WebAPI\.WindowsService\.Overview')

## DiGi\.WebAPI\.WindowsService\.Classes Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Classes.WireFormatSchemaFilter'></a>

## WireFormatSchemaFilter Class

Makes every payload schema describe the format actually written on the wire, which depends on who writes the payload\.

A DiGi `ISerializableObject` is written by the DiGi serializer - exact member names (PascalCase by convention), a mandatory `_type` discriminator, every member present - and its schema is rebuilt from that serializer's member contract ([UpdateSerializableObjectProperties\(this OpenApiSchema, SchemaFilterContext\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Modify.UpdateSerializableObjectProperties(thisMicrosoft.OpenApi.OpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext) 'DiGi\.WebAPI\.WindowsService\.Modify\.UpdateSerializableObjectProperties\(this Microsoft\.OpenApi\.OpenApiSchema, Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext\)')). Anything else (`Ok(...)` POCOs, `ProblemDetails`) is written by the MVC formatter in camelCase, and its schema is renamed to match ([CamelCasePropertyNames\(this OpenApiSchema\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Modify.CamelCasePropertyNames(thisMicrosoft.OpenApi.OpenApiSchema) 'DiGi\.WebAPI\.WindowsService\.Modify\.CamelCasePropertyNames\(this Microsoft\.OpenApi\.OpenApiSchema\)')).

See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#3; enum declaration is #6.

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