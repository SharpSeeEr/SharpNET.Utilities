using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharpNET.Core.EF.Entities
{
    public class ContactPhone
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        [MaxLength(25)]
        public string Label { get; set; }

        [MaxLength(15)]
        public string Number { get; set; }

        [MaxLength(10)]
        public string Extension { get; set; }

        public Contact Contact { get; set; }
    }
}
