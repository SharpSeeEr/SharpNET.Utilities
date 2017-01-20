using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharpNET.EF.Entities
{
    public class ContactEmail : IEntity
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        [MaxLength(25)]
        public string Label { get; set; }

        [MaxLength(254)]
        public string Email { get; set; }

        public Contact Contact { get; set; }
    }
}
