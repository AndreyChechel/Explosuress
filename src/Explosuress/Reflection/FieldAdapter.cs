using System;
using System.Reflection;

namespace Explosuress
{
    internal class FieldAdapter : FieldOrPropertyAdapter
    {
        private readonly FieldInfo _field;

        public FieldAdapter(FieldInfo field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public override object? GetValue(object? obj)
        {
            return _field!.GetValue(obj);
        }
    }
}
