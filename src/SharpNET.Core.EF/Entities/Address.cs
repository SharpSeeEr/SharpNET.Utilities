using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharpNET.Core.EF.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Label { get; set; }

        protected string _streetAddress1;

        [MaxLength(500)]
        public string StreetAddress1
        {
            get
            {
                return _streetAddress1;
            }
            set
            {
                _streetAddress1 = value;
                ParseAddress();
            }
        }

        [MaxLength(500)]
        public string StreetAddress2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(5)]
        public string Zip { get; set; }
        [MaxLength(4)]
        public string ZipPlusFour { get; set; }

        public string HouseNumber { get; private set; }
        public string Street { get; private set; }
        public string StreetType { get; private set; }
        public string ApartmentNumber { get; private set; }

        protected virtual void ParseAddress()
        {
            HouseNumber = "";
            Street = "";
            StreetType = "";
            ApartmentNumber = "";

            if (string.IsNullOrEmpty(_streetAddress1))
            {
                return;
            }

            var parts = _streetAddress1.Split(' ');
            if (parts.Length == 1)
            {
                Street = _streetAddress1;
                return;
            }
            HouseNumber = parts[0];


            // used to indicate the index of the word "apt" or "ste" is located, so we know where to stop with the streetname
            int streetLastIndex = parts.Length - 1;

            if (parts.Length >= 4)
            {
                // used to locate the actual apartment number
                int aptNumIndex = -1;
                var check = parts[parts.Length - 2].ToLower();
                var aptNum = parts[parts.Length - 1];
                if (IsApartmentIdentifier(check))
                {
                    streetLastIndex = parts.Length - 3;
                    aptNumIndex = parts.Length - 1;
                }
                else if (check == "#")
                {
                    aptNumIndex = parts.Length - 1;
                    var check2 = parts[parts.Length - 3].ToLower();
                    if (IsApartmentIdentifier(check2))
                    {
                        streetLastIndex = parts.Length - 4;
                    }
                    else
                    {
                        streetLastIndex = parts.Length - 3;
                    }
                }
                if (aptNumIndex > 0) ApartmentNumber = parts[aptNumIndex];
            }


            if (streetLastIndex >= 2 && IsStreetTypeIndicator(parts[streetLastIndex]))
            {
                StreetType = parts[streetLastIndex];
                streetLastIndex -= 1;
            }

            Street = string.Join(" ", parts.Skip(1).Take(streetLastIndex));
        }

        protected static List<string> _apartmentIdentifiers = new List<string>()
        {
            "apt",
            "apartment",
            "suite",
            "ste"
        };

        protected virtual bool IsApartmentIdentifier(string check)
        {
            check = check.ToLower();
            return _apartmentIdentifiers.Any(e => check.Contains(e));
        }


        protected static List<string> _directionIdentifiers = new List<string>()
        {
                "east", "e",
                "west", "w",
                "north", "n",
                "south", "s"
        };

        protected virtual bool IsDirectionalIndicator(string check)
        {
            return _directionIdentifiers.Contains(check.ToLower());
        }

        protected static List<string> _streetTypeIdentifiers = new List<string>()
        {
            "Avenue", "Ave",
            "Boulevard", "Blvd", "BV",
            "Circle", "Cir", "CI",
            "Crescent", "Cres", "CR",
            "Court", "Ct",
            "Drive", "Dr",
            "Garden", "Gdn",
            "Grove", "Grv",
            "Hill", "Hl",
            "Highway", "Hwy",
            "Lane", "Ln",
            "Loop", "Lp",
            "Part", "PK",
            "Place", "Pl",
            "Plaza", "Plz", "PZ",
            "Road", "Rd",
            "Run", "Rn",
            "Row", "Ro",
            "Route", "Rt",
            "Street", "St",
            "Terrace", "Ter", "TE",
            "Trail", "Trl", "Tr",
            "Way", "Wy"
        };

        protected virtual bool IsStreetTypeIndicator(string check)
        {
            return _streetTypeIdentifiers.Contains(check, StringComparer.OrdinalIgnoreCase);
        }
    }
}
