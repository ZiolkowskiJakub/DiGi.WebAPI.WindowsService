#### [DiGi\.WebAPI\.WindowsService](DiGi.WebAPI.WindowsService.Overview.md 'DiGi\.WebAPI\.WindowsService\.Overview')

## DiGi\.WebAPI\.WindowsService\.Classes\.Filters Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Classes.Filters.AddExamplesOperationFilter'></a>

## AddExamplesOperationFilter Class

Adds example values to parameters, request bodies, and responses in the OpenAPI document\.

Sets a simple example on any schema that does not already have one: the inline schema itself, or, when the
            operation refers to a component, the component schema the reference points to - OpenAPI 3.0 ignores everything
            written beside a `$ref`, and a reference has no writable example of its own.

```csharp
public class AddExamplesOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → AddExamplesOperationFilter

Implements [Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter')
### Methods

<a name='DiGi.WebAPI.WindowsService.Classes.Filters.AddExamplesOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext)'></a>

## AddExamplesOperationFilter\.Apply\(OpenApiOperation, OperationFilterContext\) Method

Sets a simple example on the schema of every parameter, request body, and response of the operation that has none\.

```csharp
public void Apply(Microsoft.OpenApi.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Classes.Filters.AddExamplesOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).operation'></a>

`operation` [Microsoft\.OpenApi\.OpenApiOperation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapioperation 'Microsoft\.OpenApi\.OpenApiOperation')

The OpenAPI operation to be modified\.

<a name='DiGi.WebAPI.WindowsService.Classes.Filters.AddExamplesOperationFilter.Apply(Microsoft.OpenApi.OpenApiOperation,Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext).context'></a>

`context` [Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.operationfiltercontext 'Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext')

The context carrying the schema repository an operation's references resolve against\.

Implements [Apply\(OpenApiOperation, OperationFilterContext\)](https://learn.microsoft.com/en-us/dotnet/api/swashbuckle.aspnetcore.swaggergen.ioperationfilter.apply#swashbuckle-aspnetcore-swaggergen-ioperationfilter-apply(microsoft-openapi-openapioperation-swashbuckle-aspnetcore-swaggergen-operationfiltercontext) 'Swashbuckle\.AspNetCore\.SwaggerGen\.IOperationFilter\.Apply\(Microsoft\.OpenApi\.OpenApiOperation,Swashbuckle\.AspNetCore\.SwaggerGen\.OperationFilterContext\)')