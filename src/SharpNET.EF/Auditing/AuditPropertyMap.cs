using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing
{
    class AuditPropertyMap
    {
        public string EntityPropertyName { get { return EntityProperty.Name; } }
        public string AuditPropertyName { get { return AuditProperty.Name; } }

        public PropertyInfo EntityProperty { get; private set; }
        public PropertyInfo AuditProperty { get; private set; }

        public AuditPropertyMap(PropertyInfo entityProperty, PropertyInfo auditProperty)
        {
            EntityProperty = entityProperty;
            AuditProperty = auditProperty;
        }
    }
}
