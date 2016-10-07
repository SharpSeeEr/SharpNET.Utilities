using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Reflection
{
    class FactoryFactoryMap<TInterface>
    {
        private Dictionary<string, Type> _map;

        public IEnumerable<string> FactoryKeys()
        {
            return _map.Keys;
        }

        public bool Contains(string key)
        {
            return _map.ContainsKey(key);
        }

        public string KeyFor(Type factoryType)
        {
            return _map.Single(f => f.Value == factoryType).Key;
        }

        public string KeyFor<TFactory>() where TFactory : TInterface
        {
            return KeyFor(typeof(TFactory));
        }

        public Type GetFactoryType(string key)
        {
            return this[key];
        }

        public Type this[string key]
        {
            get
            {
                if (!_map.ContainsKey(key)) throw new KeyNotFoundException(key);
                return _map[key];
            }
        }

        public FactoryFactoryMap(string postFix = "Factory")
        {
            var interfaceType = typeof(TInterface);

            _map = interfaceType.Assembly
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToDictionary(t => t.Name.Replace(postFix, ""), StringComparer.OrdinalIgnoreCase);
        }
    }
}
