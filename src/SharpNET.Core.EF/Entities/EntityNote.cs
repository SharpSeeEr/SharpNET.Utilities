using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.EF.Entities
{
    public abstract class EntityNote : IEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Text { get; set; }
    }
}
