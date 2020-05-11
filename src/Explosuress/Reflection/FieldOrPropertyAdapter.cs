using System.Reflection;

namespace Explosuress
{
    internal abstract class FieldOrPropertyAdapter
    {
        public static FieldOrPropertyAdapter? TryCreate(MemberInfo? member)
        {
            if (member is FieldInfo field)
            {
                return new FieldAdapter(field);
            }

            if (member is PropertyInfo property)
            {
                return new PropertyAdapter(property);
            }

            return null;
        }

        public abstract object? GetValue(object? obj);
    }
}
