using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Admin : Employee
    {
        public Admin(string? firstname = null,
                        string? lastname = null,
                        string? cnic = null,
                        string? contact = null,
                        string? gender = null,
                        string? dateofbirth = null,
                        string? town = null,
                        string? city = null,
                        string? streetaddress = null,
                        string? postalcode = null,
                        string? username = null,
                        string? password = null,
                        string? email = null,
                        double? salary = null) : base(firstname, lastname, cnic, contact, gender, dateofbirth, town, city, streetaddress, postalcode, username, password, email, salary)
        {

        }
        public Admin(List<object> args)
        {
            Id = (int)args[0];
            FirstName = (string?)args[1];
            LastName = (string?)args[2];
            CNIC = (string?)args[3];
            Contact = (string?)args[4];
            Gender = (string?)args[5];
            DateOfBirth = (string?)args[6];
            Town = (string?)args[7];
            City = (string?)args[8];
            StreetAddress = (string?)args[9];
            PostalCode = (string?)args[10];
            Username = (string?)args[11];
            Password = (string?)args[12];
            Email = (string?)args[13];
            Salary = (double)args[14];
            InitialArgs = args;
            InitialArgs.RemoveAt(0);
        }
    }
}
