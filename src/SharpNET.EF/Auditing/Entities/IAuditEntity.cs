using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing.Entities
{
    public interface IAuditEntity
    {
        DateTime Modified { get; set; }
        int ModifiedById { get; set; }
        AuditChangeType ChangeType { get; set; }
    }
}
