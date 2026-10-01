using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the member name to integer mapping of an enum as text, in numeric order: <c>Undefined = -1, Country = 0, ...</c>, with aliases of one value joined (<c>A / B = 1</c>).
        /// <para>The integers are what the DiGi serializer writes and what a client should send in a query parameter; rendered culture-invariant, so the text is identical on every machine.</para>
        /// </summary>
        /// <param name="type">The enum type, or a nullable enum type.</param>
        /// <returns>The mapping; <c>null</c> when the type is <c>null</c>, not an enum, or an enum without members.</returns>
        public static string? EnumMapping(this Type? type)
        {
            List<(object Value, List<string> Names)> members = type.EnumMembers();
            if (members.Count == 0)
            {
                return null;
            }

            return string.Join(", ", members.Select(member => string.Format(CultureInfo.InvariantCulture, "{0} = {1}", string.Join(" / ", member.Names), member.Value)));
        }
    }
}
