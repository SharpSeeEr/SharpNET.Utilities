using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing
{
    class AuditTypeMapDictionary
    {
        private Dictionary<Type, AuditTypeMap> _maps = new Dictionary<Type, AuditTypeMap>();

        public bool ContainsKey(Type type)
        {
            return _maps.ContainsKey(type);
        }

        public AuditTypeMap this[Type key]
        {
            get
            {
                return _maps[key];
            }
        }

        public void Add(AuditTypeMap map)
        {
            _maps.Add(map.EntityType, map);
        }
    }
}
