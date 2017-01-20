using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharpNET.EF.Entities
{
    public class Contact : IEntity
    {
        public Contact()
        {
            Addresses = new List<Address>();
            PhoneNumbers = new List<ContactPhone>();
            EmailAddresses = new List<ContactEmail>();
            CustomFields = new List<ContactField>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }

        public string DisplayAs { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ContactPhone> PhoneNumbers { get; set; }
        public virtual ICollection<ContactEmail> EmailAddresses { get; set; }
        public virtual ICollection<ContactField> CustomFields { get; set; }
    }
}
