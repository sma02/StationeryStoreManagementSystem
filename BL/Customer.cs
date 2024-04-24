using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Customer:AbstractUser
    {
        public double PaymentDues {  get; set; }
    }
}
