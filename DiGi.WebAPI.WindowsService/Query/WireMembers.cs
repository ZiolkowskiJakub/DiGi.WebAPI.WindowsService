using System;
using System.Collections.Generic;
using System.Reflection;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the members the DiGi serializer writes for an instance of the given type, each with the JSON property name it is written under, in the order it writes them.
        /// <para>Mirrors <c>DiGi.Core.Create.SerializationMethodCollection</c> and <c>SerializationMethodCollection.Create</c>: members come from <c>Core.Query.SerializableMemberInfos</c> (base type first), are named by <c>Core.Query.SerializableName</c>, members carrying a <c>[JsonPropertyOrder]</c> go first in that order, a later member replaces an earlier one of the same name in its position, and a property without a parameterless getter is dropped because the serializer cannot read it. Keep the two in step - a schema built from this list describes the wire only while they agree.</para>
        /// <para>Valid only for types whose JSON is built from these members; see <see cref="HasMemberWireFormat"/>.</para>
        /// </summary>
        /// <param name="type">The serializable type.</param>
        /// <returns>The written members with their JSON names; empty when the type is <c>null</c> or has no serializable members.</returns>
        public static List<(string Name, MemberInfo MemberInfo)> WireMembers(this Type? type)
        {
            List<MemberInfo>? memberInfos = Core.Query.SerializableMemberInfos(type);
            if (memberInfos is null || memberInfos.Count == 0)
            {
                return [];
            }

            List<(string Name, MemberInfo MemberInfo)> members = [];
            List<(string Name, MemberInfo MemberInfo, int Order)> members_Ordered = [];
            foreach (MemberInfo memberInfo in memberInfos)
            {
                string? name = Core.Query.SerializableName(memberInfo);
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                int? order = Core.Query.SerializableOrder(memberInfo);
                if (order is null)
                {
                    members.Add((name, memberInfo));
                }
                else
                {
                    members_Ordered.Add((name, memberInfo, order.Value));
                }
            }

            members_Ordered.Sort((x, y) => x.Order.CompareTo(y.Order));
            members.InsertRange(0, members_Ordered.ConvertAll(x => (x.Name, x.MemberInfo)));

            List<(string Name, MemberInfo MemberInfo)> result = [];
            Dictionary<string, int> indexes = [];
            foreach ((string Name, MemberInfo MemberInfo) member in members)
            {
                if (indexes.TryGetValue(member.Name, out int index))
                {
                    result[index] = member;
                }
                else
                {
                    indexes[member.Name] = result.Count;
                    result.Add(member);
                }
            }

            result.RemoveAll(member => member.MemberInfo is PropertyInfo propertyInfo && (propertyInfo.GetMethod is null || propertyInfo.GetMethod.GetParameters().Length != 0));

            return result;
        }
    }
}
