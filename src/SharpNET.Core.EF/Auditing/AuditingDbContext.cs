using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharpNET.EF.Auditing.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace SharpNET.EF.Auditing
{
    public class AuditingDbContext : DbContext
    {
        private static AuditTypeMapDictionary _auditMaps = new AuditTypeMapDictionary();
        
        public AuditingDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public override int SaveChanges()
        {
            CreateAuditEntries();
            return base.SaveChanges();
        }

        private void CreateAuditEntries()
        {
            // In On Model Creating, enable auditing for entities
            // When saving, check if any of the entities with auditing enabled are being added or changed
            // For each of those entities, create the audit entity and add to the context
            // How do we get the Id of the entity to put on the AuditEntity?

            // Process any auditable objects.
            var auditedEntries = ChangeTracker.Entries<IAuditedEntity>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var auditedEntry in auditedEntries)
            {
                var entityType = auditedEntry.Entity.GetType();

                if (!_auditMaps.ContainsKey(entityType))
                {
                    continue;
                }

                var map = _auditMaps[entityType];

                var auditEntity = CreateAuditEntity(auditedEntry, map);
            }
        }

        private IAuditEntity CreateAuditEntity(EntityEntry entityEntry, AuditTypeMap map)
        {
            var entity = entityEntry.Entity as IAuditedEntity;

            //MethodInfo method = typeof(Example).GetMethod("Test");
            //MethodInfo genericMethod = method.MakeGenericMethod(typeof(string));
            //genericMethod.Invoke(this, null);
            var auditEntity = map.CreateAuditInstance();
            var auditEntry = Add(auditEntity);

            map.Map(entityEntry, auditEntry);
            
            return auditEntity as IAuditEntity;
        }

        /// <summary>
        /// Registers an auditable entity with it's audit entity
        /// </summary>
        /// <typeparam name="TAuditedEntity">Type to audit, must implement IAuditedEntity</typeparam>
        /// <typeparam name="TAuditEntity">Type of audit entity, must implement IAuditEntity</typeparam>
        public void RegisterAuditedType<TAuditedEntity, TAuditEntity>()
        {
            RegisterAuditedType(typeof(TAuditedEntity), typeof(TAuditEntity));
        }

        /// <summary>
        /// Registers an auditable entity with it's audit entity
        /// </summary>
        /// <param name="entityType">Type to audit, must implement IAuditedEntity.</param>
        /// <param name="auditType">Type of audit entity, must implement IAuditEntity.</param>
        public void RegisterAuditedType(Type entityType, Type auditType)
        {
            // Basic parameter validation.
            if (entityType == null)
            {
                throw new ArgumentNullException("entityType");
            }

            if (auditType == null)
            {
                throw new ArgumentNullException("auditType");
            }

            if (_auditMaps.ContainsKey(entityType))
            {
                if (_auditMaps[entityType].AuditType == auditType) return;
                throw new ArgumentException("Type already registered for auditing with a different audit entity.", "entityType");
            }

            if (!typeof(IAuditedEntity).GetTypeInfo().IsAssignableFrom(entityType))
            {
                throw new ArgumentException("Audited Entity does not implement " + typeof(IAuditedEntity).Name, "entityType");
            }

            if (!typeof(IAuditEntity).GetTypeInfo().IsAssignableFrom(auditType))
            {
                throw new ArgumentException("Audit Entity does not implement " + typeof(IAuditEntity).Name, "auditType");
            }

            var map = new AuditTypeMap(entityType, auditType);
            _auditMaps.Add(map);
        }
    }
}
