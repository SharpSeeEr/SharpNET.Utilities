using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing.Entities
{
    public abstract class AuditEntity : IAuditEntity
    {
        public DateTime Modified { get; set; }
        public int ModifiedById { get; set; }
        public AuditChangeType ChangeType { get; set; }
    }
}
