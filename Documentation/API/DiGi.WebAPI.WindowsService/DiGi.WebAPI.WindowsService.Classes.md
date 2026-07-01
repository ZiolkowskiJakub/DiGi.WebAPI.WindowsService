#### [DiGi\.WebAPI\.WindowsService](index.md 'index')

## DiGi\.WebAPI\.WindowsService\.Classes Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Classes.CamelCaseSchemaFilter'></a>

## CamelCaseSchemaFilter Class

Provides a schema filter that converts property names in the OpenAPI schema to camelCase\.

```csharp
public class CamelCaseSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → CamelCaseSchemaFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.ISchemaFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ischemafilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.ISchemaFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Classes.CamelCaseSchemaFilter.Apply(Microsoft.OpenApi.IOpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext)'></a>

## CamelCaseSchemaFilter\.Apply\(IOpenApiSchema, SchemaFilterContext\) Method

Applies the camelCase naming policy to the properties of the provided OpenAPI schema\.

```csharp
public void Apply(Microsoft.OpenApi.IOpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Classes.CamelCaseSchemaFilter.Apply(Microsoft.OpenApi.IOpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext).schema'></a>

`schema` [Microsoft\.OpenApi\.IOpenApiSchema](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.iopenapischema 'Microsoft\.OpenApi\.IOpenApiSchema')

The OpenAPI schema to be modified\.

<a name='DiGi.WebAPI.WindowsService.Classes.CamelCaseSchemaFilter.Apply(Microsoft.OpenApi.IOpenApiSchema,Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.schemafiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext')

The context containing information about the schema being filtered\.

Implements [Apply\(IOpenApiSchema, SchemaFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ischemafilter.apply#swashbuckle-aspnetcore-swaggergen-ischemafilter-apply(microsoft-openapi-iopenapischema-swashbuckle-aspnetcore-swaggergen-schemafiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.ISchemaFilter\.Apply\(Microsoft\.OpenApi\.IOpenApiSchema,Swashbuckle\.AspNetCore\.SwaggerGen\.SchemaFilterContext\)')