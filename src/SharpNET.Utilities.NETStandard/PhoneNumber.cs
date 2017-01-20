using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities
{
    public class PhoneNumber
    {
        public PhoneNumber()
        {

        }

        public PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public string Number { get; set; }

        public string AreaCode
        {
            get
            {
                return Number.Substring(0, 3);
            }
        }

        public string Prefix
        {
            get
            {
                return Number.Substring(3, 3);
            }
        }

        public string Postfix
        {
            get
            {
                return Number.Substring(6);
            }
        }

        public string Formatted
        {
            get
            {
                return Number.ToPhone();
            }
        }
    }
}
