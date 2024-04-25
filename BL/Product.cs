using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Company? Company { get; set; }
        public int? ReorderThreshold { get; set; }
        public Category? Category { get; set; }
        public List<object> InitialArgs { get; set; }

        public Product(int id, string? name, Company? company, int? reorderThreshold, Category? category)
        {
            Id = id;
            Name = name;
            Company = company;
            ReorderThreshold = reorderThreshold;
            Category = category;
        }
        public Product(List<object> args) 
        {
            Id = (int)args[0];
            Name = (string)args[1];
            Company = (Company)args[2];
            ReorderThreshold = (int)args[3];
            Category = (Category)args[4];
            InitialArgs = args;
            InitialArgs.RemoveAt(0);
        }
    }
}
