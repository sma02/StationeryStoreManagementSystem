using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace StationeryStoreManagementSystem.BL
{
    class Employee:AbstractUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public bool isAdmin { get; set; }
    }
}
