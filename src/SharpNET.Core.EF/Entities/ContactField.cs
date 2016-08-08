using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharpNET.Core.EF.Entities
{
    public class ContactField
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        [MaxLength(25)]
        public string Label { get; set; }

        [MaxLength(1000)]
        public string Value { get; set; }

        public Contact Contact { get; set; }
    }
}
