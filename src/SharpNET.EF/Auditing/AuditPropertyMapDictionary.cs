using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing
{
    class AuditPropertyMapDictionary : Dictionary<string, AuditPropertyMap>
    {
        public void Add(PropertyInfo entityProperty, PropertyInfo auditProperty)
        {
            Add(entityProperty.Name, new AuditPropertyMap(entityProperty, auditProperty));
        }

        public void Add(AuditPropertyMap map)
        {
            Add(map.EntityProperty.Name, map);
        }
    }
}
