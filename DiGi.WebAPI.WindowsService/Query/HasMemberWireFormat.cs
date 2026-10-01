using DiGi.Core.Classes;
using System;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Checks whether the JSON the DiGi serializer writes for a type is built from that type's own serializable members (<see cref="WireMembers"/>), so that a schema can list them.
        /// <para>Not so for an interface or an abstract type, whose payloads carry the members of whichever concrete type was serialized (named by <c>_type</c>), nor for a type that overrides <see cref="SerializableObject.ToJsonObject"/> (<c>SerializableObjectWrapper</c>, <c>IndexedObjects&lt;T&gt;</c>, <c>Matrix</c>, ...) or implements <c>ISerializableObject</c> without deriving from <see cref="SerializableObject"/>, whose JSON is written by its own code.</para>
        /// <para>Whether the listed members are also all the payload can carry is a separate question: a member declared as this type may hold a subclass (<see cref="DerivedTypes"/>).</para>
        /// </summary>
        /// <param name="type">The serializable type.</param>
        /// <returns><c>true</c> when the type is a concrete <see cref="SerializableObject"/> serialized through the default member contract; otherwise <c>false</c>.</returns>
        public static bool HasMemberWireFormat(this Type? type)
        {
            if (type is null || type.IsInterface || type.IsAbstract || !typeof(SerializableObject).IsAssignableFrom(type))
            {
                return false;
            }

            return type.GetMethod(nameof(SerializableObject.ToJsonObject), Type.EmptyTypes)?.DeclaringType == typeof(SerializableObject);
        }
    }
}
