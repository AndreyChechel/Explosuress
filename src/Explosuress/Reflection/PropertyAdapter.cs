using System;
using System.Reflection;

namespace Explosuress
{
    internal class PropertyAdapter : FieldOrPropertyAdapter
    {
        private readonly PropertyInfo _property;

        public PropertyAdapter(PropertyInfo property)
        {
            _property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public override object? GetValue(object? obj)
        {
            return _property!.GetValue(obj);
        }
    }
}
