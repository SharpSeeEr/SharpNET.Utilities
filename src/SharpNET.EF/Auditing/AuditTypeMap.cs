using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing
{
    class AuditTypeMap
    {
        /// <summary>
        /// Initializes a new instance of the AuditTypeInfo class.
        /// </summary>
        public AuditTypeMap(Type entityType, Type auditType)
        {
            EntityType = entityType;
            AuditType = auditType;
            PropertyMap = new AuditPropertyMapDictionary();

            InitPropertyMap();
        }

        public Type EntityType { get; private set; }

        public Type AuditType { get; private set; }

        public void Map(DbEntityEntry entityEntry, DbEntityEntry auditEntry)
        {
            foreach (var propMap in PropertyMap.Values)
            {
                try
                {
                    var originalValue = entityEntry.Property(propMap.EntityProperty.Name).OriginalValue;
                    auditEntry.Property(propMap.AuditProperty.Name).CurrentValue = originalValue;
                }
                catch (Exception)
                {
                    // Ignoring
                }
            }

            auditEntry.Property("ChangeType").CurrentValue = _changeTypeMap[entityEntry.State];
        }

        public object CreateAuditInstance()
        {
            return Activator.CreateInstance(AuditType);
        }

        private Dictionary<EntityState, AuditChangeType> _changeTypeMap = new Dictionary<EntityState, AuditChangeType>()
        {
            { EntityState.Added, AuditChangeType.Added },
            { EntityState.Deleted, AuditChangeType.Deleted },
            { EntityState.Modified, AuditChangeType.Modified }
        };

        public AuditPropertyMapDictionary PropertyMap { get; set; }

        private void InitPropertyMap()
        {
            // Extract the list of properties to audit.
            var auditProperties = AuditType.GetProperties().ToDictionary(x => x.Name);

            foreach (var entityProperty in EntityType.GetProperties())
            {
                var name = entityProperty.Name;
                
                // Id maps to <classname>Id
                if (name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    name = EntityType.Name + "Id";
                }

                // We need to ignore the 
                if (auditProperties.ContainsKey(name) && entityProperty.PropertyType == auditProperties[name].PropertyType)
                {
                    PropertyMap.Add(entityProperty, auditProperties[name]);
                }
            }
        }

        

        
    }
}
