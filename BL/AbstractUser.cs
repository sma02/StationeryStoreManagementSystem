using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    abstract class AbstractUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public KeyValuePair<int,string>? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Town { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }

        public void Save(bool isAdd = false)
        {
            
        }
    }
}
