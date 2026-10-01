using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the members of an enum by the value each one travels the wire as: one entry per distinct value, in numeric order, naming every member declared with that value in declaration order (the first name is the member's own, the rest are aliases).
        /// <para>The value is the member's underlying integer, boxed as the enum's underlying type (an <c>int</c> for an <c>int</c>-backed enum) - what the DiGi serializer writes. Ordered numerically, not as <see cref="Enum.GetValues(Type)"/> returns them: that orders by the unsigned bit pattern and puts a negative value such as <c>Undefined = -1</c> last.</para>
        /// </summary>
        /// <param name="type">The enum type, or a nullable enum type.</param>
        /// <returns>The distinct values with their member names; empty when the type is <c>null</c>, not an enum, or an enum without members.</returns>
        public static List<(object Value, List<string> Names)> EnumMembers(this Type? type)
        {
            Type? type_Enum = type is null ? null : Nullable.GetUnderlyingType(type) ?? type;
            if (type_Enum is null || !type_Enum.IsEnum)
            {
                return [];
            }

            List<(object Value, decimal Order, List<string> Names)> members = [];
            Dictionary<decimal, int> indexes = [];
            foreach (FieldInfo fieldInfo in type_Enum.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                object? value = fieldInfo.GetRawConstantValue();
                if (value is null)
                {
                    continue;
                }

                // decimal holds every underlying type exactly, ulong above long.MaxValue included.
                decimal order = System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                if (indexes.TryGetValue(order, out int index))
                {
                    members[index].Names.Add(fieldInfo.Name);
                    continue;
                }

                indexes[order] = members.Count;
                members.Add((value, order, [fieldInfo.Name]));
            }

            members.Sort((x, y) => x.Order.CompareTo(y.Order));

            return members.ConvertAll(member => (member.Value, member.Names));
        }
    }
}
