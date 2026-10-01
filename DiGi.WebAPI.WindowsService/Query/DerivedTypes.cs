using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the types loaded into the application domain that derive from (or implement) the given type, the type itself excluded.
        /// <para>The host loads every extension assembly at start-up, so after start-up this is every type a payload declared as <paramref name="type"/> can actually hold - <c>EPWFile.WeatherRecords</c> is declared as a list of <c>WeatherRecord</c> and holds <c>DataRecord</c>s from <c>DiGi.EPW</c>. Only the type's own assembly and the assemblies referencing it are scanned; a type that fails to load is skipped.</para>
        /// </summary>
        /// <param name="type">The base type or interface.</param>
        /// <returns>The derived types; empty for <c>null</c>, a sealed type or a value type.</returns>
        public static List<Type> DerivedTypes(this Type? type)
        {
            if (type is null || type.IsSealed || type.IsValueType)
            {
                return [];
            }

            string? assemblyName = type.Assembly.GetName().Name;

            List<Type> result = [];
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDynamic)
                {
                    continue;
                }

                if (assembly != type.Assembly && !assembly.GetReferencedAssemblies().Any(referencedAssemblyName => referencedAssemblyName.Name == assemblyName))
                {
                    continue;
                }

                Type?[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException reflectionTypeLoadException)
                {
                    types = reflectionTypeLoadException.Types;
                }

                foreach (Type? type_Derived in types)
                {
                    if (type_Derived is not null && type_Derived != type && type.IsAssignableFrom(type_Derived))
                    {
                        result.Add(type_Derived);
                    }
                }
            }

            return result;
        }
    }
}
