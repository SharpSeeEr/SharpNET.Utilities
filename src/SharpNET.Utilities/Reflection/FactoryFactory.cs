using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Reflection
{
    public class FactoryFactory<TInterface>
    {
        private static FactoryFactoryMap<TInterface> _factoryMap;
        private static Dictionary<string, TInterface> _factories;

        static FactoryFactory()
        {
            _factoryMap = new FactoryFactoryMap<TInterface>();
            _factories = new Dictionary<string, TInterface>();
        }

        public TInterface GetFactory(string key)
        {

            if (!_factoryMap.Contains(key))
            {
                throw new KeyNotFoundException(key);
            }

            if (!_factories.ContainsKey(key))
            {
                var type = _factoryMap[key];
                _factories.Add(key, (TInterface)Activator.CreateInstance(type));
            }
            return _factories[key];
        }
    }
}
