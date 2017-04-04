using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Auditing.Entities
{
    public interface IAuditedEntity : IEntity
    {
        //int Id { get; set; }
        DateTime Created { get; set; }
        int CreatedById { get; set; }
        DateTime Modified { get; set; }
        int ModifiedById { get; set; }
    }
}
