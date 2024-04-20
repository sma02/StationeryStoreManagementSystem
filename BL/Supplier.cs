using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
        public Supplier(int id
                       ,string? name = null
                       ,string? contact = null
                       ,string? email = null
                       ,Address? address = null)
        {
            Id = id;
            Name = name;
            Contact = contact;
            Email = email;
            Address = address;

        }
        public Supplier(List<object> args)
        {
            Id = (int)args[0];
            Name = (string?)args[1];
            Contact = (string?)args[2];
            Email = (string?)args[3];
            Address = new Address((string)args[4],
                                  (string)args[5],
                                  (string)args[6],
                                  (string)args[7],
                                  (string)args[8]);
        }
    }
}
