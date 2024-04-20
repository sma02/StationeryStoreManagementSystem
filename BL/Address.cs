using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    enum Cities
    {
        Lahore,
        Karachi,
        Peshawar
    }
    class Address
    {
        public string StreetAddress { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Address(string streetAddress, string town, string city, string postalCode, string country = null)
        {
            StreetAddress = streetAddress;
            Town = town;
            City = city;
            Country = country;
            PostalCode = postalCode;
        }
    }
}
