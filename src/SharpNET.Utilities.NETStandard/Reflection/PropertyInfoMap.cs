using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Reflection
{
    public class PropertyInfoMap : Dictionary<string, PropertyInfo>
    {
        private static readonly object _locker = new object();
        private static Dictionary<Type, PropertyInfoMap> _lookup = new Dictionary<Type, PropertyInfoMap>();

        public Type Type { get; private set; }

        private PropertyInfoMap(Type type)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            Type = type;

            foreach (var item in Type.GetProperties())
            {
                Add(item.Name, item);
            }
        }

        public static PropertyInfoMap For(Type type)
        {
            if (!_lookup.ContainsKey(type))
            {
                lock (_locker)
                {
                    if (!_lookup.ContainsKey(type))
                    {
                        _lookup.Add(type, new PropertyInfoMap(type));
                    }
                }
            }
            return _lookup[type];
        }

        public static PropertyInfoMap For<T>()
        {
            return For(typeof(T));
        }
    }
}
