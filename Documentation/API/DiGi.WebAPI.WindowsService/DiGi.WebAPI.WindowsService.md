#### [DiGi\.WebAPI\.WindowsService](DiGi.WebAPI.WindowsService.Overview.md 'DiGi\.WebAPI\.WindowsService\.Overview')

## DiGi\.WebAPI\.WindowsService Namespace
### Classes

<a name='DiGi.WebAPI.WindowsService.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.WebAPI.WindowsService.Create.OpenApiSchema(thisSystem.Type,string,bool)'></a>

## Create\.OpenApiSchema\(this Type, string, bool\) Method

Creates the inline schema of an enum as the DiGi serializer writes it: an integer, the member's underlying value\.

The values are listed in numeric order ([EnumMembers\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.EnumMembers(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.EnumMembers\(this System\.Type\)')), named for code generators in the same order by `x-enum-varnames` (openapi-generator) and `x-enumNames` (NSwag), and mapped to the member names in the description. The format is `int64` for a `long`, `uint` or `ulong` enum, `int32` otherwise, and left out when a `ulong` value exceeds `int64`.

A nullable enum gets the `null` type and `null` last among the values: OpenAPI 3.0's `nullable` widens `type` only, so a value list without `null` would still reject the explicit `null` the serializer writes. A `[Flags]` enum lists no values - a combined value is in no member list - and describes its bits instead; an enum without members lists no values either, because an empty list would reject everything.

See ZiolkowskiJakub/DiGi.WebAPI.WindowsService#6: the shared enum component stays the string schema query parameters bind against, so a DiGi payload member declares its enum inline.

```csharp
public static Microsoft.OpenApi.OpenApiSchema? OpenApiSchema(this System.Type? type, string? description=null, bool nullable=false);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Create.OpenApiSchema(thisSystem.Type,string,bool).type'></a>

`type` [System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')

The enum type, or a nullable enum type \(which makes the schema nullable\)\.

<a name='DiGi.WebAPI.WindowsService.Create.OpenApiSchema(thisSystem.Type,string,bool).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The description to start with \- the member's and the enum's; the wire mapping is appended to it\.

<a name='DiGi.WebAPI.WindowsService.Create.OpenApiSchema(thisSystem.Type,string,bool).nullable'></a>

`nullable` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Whether the value may be `null`, besides a nullable enum type\.

#### Returns
[Microsoft\.OpenApi\.OpenApiSchema](https://learn.microsoft.com/en-us/dotnet/api/microsoft.openapi.openapischema 'Microsoft\.OpenApi\.OpenApiSchema')  
The inline integer schema; `null` when the type is `null` or not an enum\.

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

<a name='DiGi.WebAPI.WindowsService.Query.DerivedTypes(thisSystem.Type)'></a>

## Query\.DerivedTypes\(this Type\) Method

Gets the types loaded into the application domain that derive from \(or implement\) the given type, the type itself excluded\.

The host loads every extension assembly at start-up, so after start-up this is every type a payload declared as [type](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.DerivedTypes(thisSystem.Type).type 'DiGi\.WebAPI\.WindowsService\.Query\.DerivedTypes\(this System\.Type\)\.type') can actually hold - `EPWFile.WeatherRecords` is declared as a list of `WeatherRecord` and holds `DataRecord`s from `DiGi.EPW`. Only the type's own assembly and the assemblies referencing it are scanned; a type that fails to load is skipped.

```csharp
public static System.Collections.Generic.List<System.Type> DerivedTypes(this System.Type? type);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.DerivedTypes(thisSystem.Type).type'></a>

`type` [System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')

The base type or interface\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The derived types; empty for `null`, a sealed type or a value type\.

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

<a name='DiGi.WebAPI.WindowsService.Query.EnumMapping(thisSystem.Type)'></a>

## Query\.EnumMapping\(this Type\) Method

Gets the member name to integer mapping of an enum as text, in numeric order: `Undefined = -1, Country = 0, ...`, with aliases of one value joined \(`A / B = 1`\)\.

The integers are what the DiGi serializer writes and what a client should send in a query parameter; rendered culture-invariant, so the text is identical on every machine.

```csharp
public static string? EnumMapping(this System.Type? type);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.EnumMapping(thisSystem.Type).type'></a>

`type` [System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')

The enum type, or a nullable enum type\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The mapping; `null` when the type is `null`, not an enum, or an enum without members\.

<a name='DiGi.WebAPI.WindowsService.Query.EnumMembers(thisSystem.Type)'></a>

## Query\.EnumMembers\(this Type\) Method

Gets the members of an enum by the value each one travels the wire as: one entry per distinct value, in numeric order, naming every member declared with that value in declaration order \(the first name is the member's own, the rest are aliases\)\.

The value is the member's underlying integer, boxed as the enum's underlying type (an `int` for an `int`-backed enum) - what the DiGi serializer writes. Ordered numerically, not as [System\.Enum\.GetValues\(System\.Type\)](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues#system-enum-getvalues(system-type) 'System\.Enum\.GetValues\(System\.Type\)') returns them: that orders by the unsigned bit pattern and puts a negative value such as `Undefined = -1` last.

```csharp
public static System.Collections.Generic.List<(object Value,System.Collections.Generic.List<string> Names)> EnumMembers(this System.Type? type);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.EnumMembers(thisSystem.Type).type'></a>

`type` [System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')

The enum type, or a nullable enum type\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')[,](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The distinct values with their member names; empty when the type is `null`, not an enum, or an enum without members\.

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

<a name='DiGi.WebAPI.WindowsService.Query.HasMemberWireFormat(thisSystem.Type)'></a>

## Query\.HasMemberWireFormat\(this Type\) Method

Checks whether the JSON the DiGi serializer writes for a type is built from that type's own serializable members \([WireMembers\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.WireMembers(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.WireMembers\(this System\.Type\)')\), so that a schema can list them\.

Not so for an interface or an abstract type, whose payloads carry the members of whichever concrete type was serialized (named by `_type`), nor for a type that overrides [DiGi\.Core\.Classes\.SerializableObject\.ToJsonObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject.tojsonobject 'DiGi\.Core\.Classes\.SerializableObject\.ToJsonObject') (`SerializableObjectWrapper`, `IndexedObjects<T>`, `Matrix`, ...) or implements `ISerializableObject` without deriving from [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject'), whose JSON is written by its own code.

Whether the listed members are also all the payload can carry is a separate question: a member declared as this type may hold a subclass ([DerivedTypes\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.DerivedTypes(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.DerivedTypes\(this System\.Type\)')).

```csharp
public static bool HasMemberWireFormat(this System.Type? type);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.HasMemberWireFormat(thisSystem.Type).type'></a>

`type` [System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')

The serializable type\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` when the type is a concrete [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') serialized through the default member contract; otherwise `false`\.

<a name='DiGi.WebAPI.WindowsService.Query.WireMembers(thisSystem.Type)'></a>

## Query\.WireMembers\(this Type\) Method

Gets the members the DiGi serializer writes for an instance of the given type, each with the JSON property name it is written under, in the order it writes them\.

Mirrors `DiGi.Core.Create.SerializationMethodCollection` and `SerializationMethodCollection.Create`: members come from `Core.Query.SerializableMemberInfos` (base type first), are named by `Core.Query.SerializableName`, members carrying a `[JsonPropertyOrder]` go first in that order, a later member replaces an earlier one of the same name in its position, and a property without a parameterless getter is dropped because the serializer cannot read it. Keep the two in step - a schema built from this list describes the wire only while they agree.

Valid only for types whose JSON is built from these members; see [HasMemberWireFormat\(this Type\)](DiGi.WebAPI.WindowsService.md#DiGi.WebAPI.WindowsService.Query.HasMemberWireFormat(thisSystem.Type) 'DiGi\.WebAPI\.WindowsService\.Query\.HasMemberWireFormat\(this System\.Type\)').

```csharp
public static System.Collections.Generic.List<(string Name,System.Reflection.MemberInfo MemberInfo)> WireMembers(this System.Type? type);
```
#### Parameters

<a name='DiGi.WebAPI.WindowsService.Query.WireMembers(thisSystem.Type).type'></a>

`type` [System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')

The serializable type\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[System\.Reflection\.MemberInfo](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo 'System\.Reflection\.MemberInfo')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System\.ValueTuple')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The written members with their JSON names; empty when the type is `null` or has no serializable members\.