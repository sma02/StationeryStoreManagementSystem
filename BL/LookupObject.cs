using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class LookupObject
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public LookupObject(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
