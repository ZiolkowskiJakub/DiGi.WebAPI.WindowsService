#### [DiGi\.WebAPI\.WindowsService](DiGi.WebAPI.WindowsService.Overview.md 'DiGi\.WebAPI\.WindowsService\.Overview')

## DiGi\.WebAPI\.WindowsService Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.WebAPI.WindowsService.Modify.InitializeAsync(thisSystem.Reflection.Assembly,Microsoft.Extensions.DependencyInjection.IServiceCollection)'></a>

## Modify\.InitializeAsync\(this Assembly, IServiceCollection\) Method

Initializes the specified assembly by scanning for classes inheriting from [DiGi\.WebAPI\.Classes\.WebAPIController](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.webapicontroller 'DiGi\.WebAPI\.Classes\.WebAPIController') 
and executing any static initialization methods named "Initialize" or "InitializeAsync" within a class named "Modify"\.

```csharp
public static System.Threading.Tasks.Task<bool> InitializeAsync(this System.Reflection.Assembly? assembly, Microsoft.Extensions.DependencyInjection.IServiceCollection? serviceCollection);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Modify.InitializeAsync(thisSystem.Reflection.Assembly,Microsoft.Extensions.DependencyInjection.IServiceCollection).assembly'></a>

`assembly` [System\.Reflection\.Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly 'System\.Reflection\.Assembly')

The assembly to be scanned for controllers and initialization logic\.

<a name='DiGi.WebAPI.WindowsService.Modify.InitializeAsync(thisSystem.Reflection.Assembly,Microsoft.Extensions.DependencyInjection.IServiceCollection).serviceCollection'></a>

`serviceCollection` [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection')

The [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection') used to register services and controllers\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if 
            controllers were registered or initialization methods were successfully executed; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.WebAPI.WindowsService.Program'></a>

## Program Class

Provides the entry point and installation logic for the DiGi WebAPI Windows Service\.

```csharp
public class Program
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Program
### Methods

<a name='DiGi.WebAPI.WindowsService.Program.Install()'></a>

## Program\.Install\(\) Method

Installs the application as a Windows Service using the system service controller \(sc\.exe\)\.

```csharp
public static System.Threading.Tasks.Task Install();
```

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')  
A [System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task') representing the asynchronous operation\.

<a name='DiGi.WebAPI.WindowsService.Program.Main(string[])'></a>

## Program\.Main\(string\[\]\) Method

The main entry point of the application which determines whether to install, uninstall, or run the service based on command\-line arguments\.

```csharp
public static System.Threading.Tasks.Task Main(string[] args);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Program.Main(string[]).args'></a>

`args` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

An array of command\-line arguments passed to the application\.

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')  
A [System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task') representing the asynchronous operation\.

<a name='DiGi.WebAPI.WindowsService.Program.Run(string[])'></a>

## Program\.Run\(string\[\]\) Method

Configures and executes the Web API as a Windows Service, including environment setup, logging initialization, and service lifetime configuration\.

```csharp
public static System.Threading.Tasks.Task Run(string[] args);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Program.Run(string[]).args'></a>

`args` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

An array of command\-line arguments used to configure the web application options\.

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')  
A [System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task') representing the asynchronous operation\.

<a name='DiGi.WebAPI.WindowsService.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.WebAPI.WindowsService.Query.DocumentName(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.ApiDescription)'></a>

## Query\.DocumentName\(this ApiDescription\) Method

Gets the name of the Swagger document an API description belongs to: the first segment of its relative path, in lower case\.

The segment is lower-cased explicitly because the `[controller]` route token expands to the controller name as declared (`User`), while `LowercaseUrls` only affects generated links.

```csharp
public static string? DocumentName(this Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription? apiDescription);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.DocumentName(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.ApiDescription).apiDescription'></a>

`apiDescription` [Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.ApiDescription](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apiexplorer.apidescription 'Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.ApiDescription')

The API description to classify\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The lower\-case first route segment \(for example `gis` for `gis/Building2D/items`\), or `null` when the description or its relative path is missing or the first segment is a route parameter \(`{id}`\); such an endpoint appears in the full document only\.

<a name='DiGi.WebAPI.WindowsService.Query.DocumentNames(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider)'></a>

## Query\.DocumentNames\(this IApiDescriptionGroupCollectionProvider\) Method

Gets the names of the per\-route\-prefix Swagger documents: the distinct first route segments of every described endpoint\.

The name of the full document ([SwaggerDocument\_Full](DiGi.WebAPI.WindowsService.Constants.md#DiGi.WebAPI.WindowsService.Constants.Name.SwaggerDocument_Full 'DiGi\.WebAPI\.WindowsService\.Constants\.Name\.SwaggerDocument\_Full')) is excluded, so a route prefix of that name cannot replace it.

```csharp
public static System.Collections.Generic.List<string> DocumentNames(this Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider? apiDescriptionGroupCollectionProvider);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.DocumentNames(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider).apiDescriptionGroupCollectionProvider'></a>

`apiDescriptionGroupCollectionProvider` [Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.IApiDescriptionGroupCollectionProvider](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apiexplorer.iapidescriptiongroupcollectionprovider 'Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.IApiDescriptionGroupCollectionProvider')

The provider of the API descriptions of every loaded controller, extensions included\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The document names in ordinal order; empty when the provider is `null`\.

<a name='DiGi.WebAPI.WindowsService.Query.DocumentVersion(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider,string)'></a>

## Query\.DocumentVersion\(this IApiDescriptionGroupCollectionProvider, string\) Method

Gets the version of the assembly whose controllers serve the endpoints of the given Swagger document\.

When several assemblies share the route prefix, the highest version is returned.

```csharp
public static string? DocumentVersion(this Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider? apiDescriptionGroupCollectionProvider, string? documentName);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.DocumentVersion(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider,string).apiDescriptionGroupCollectionProvider'></a>

`apiDescriptionGroupCollectionProvider` [Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.IApiDescriptionGroupCollectionProvider](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apiexplorer.iapidescriptiongroupcollectionprovider 'Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.IApiDescriptionGroupCollectionProvider')

The provider of the API descriptions of every loaded controller, extensions included\.

<a name='DiGi.WebAPI.WindowsService.Query.DocumentVersion(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider,string).documentName'></a>

`documentName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The document name, as returned by [DocumentName\(this ApiDescription\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.DocumentName(thisMicrosoft.AspNetCore.Mvc.ApiExplorer.ApiDescription) 'DiGi\.WebAPI\.WindowsService\.Query\.DocumentName\(this Microsoft\.AspNetCore\.Mvc\.ApiExplorer\.ApiDescription\)')\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The three\-part assembly version \(for example `0.8.8`\), or `null` when no controller serves the document or its assembly carries no version\.

<a name='DiGi.WebAPI.WindowsService.Query.ExcludedLibrary(string)'></a>

## Query\.ExcludedLibrary\(string\) Method

Determines whether the specified library path should be excluded based on standard system and Microsoft naming conventions\.

```csharp
public static bool ExcludedLibrary(string? path);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.ExcludedLibrary(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The file path of the library to check for exclusion\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the library is a system or Microsoft assembly; otherwise, false\.