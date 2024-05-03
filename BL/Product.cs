using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company? Company { get; set; }
        public int? ReorderThreshold { get; set; }
        public Category? Category { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<object> InitialArgs { get; set; }
        public int SupplierQuantity
        {
            get
            {
                if (Stocks != null && Stocks.Count > 0)
                    return Stocks[0].Quantity;
                return 0;
            }
            set
            {
                if (Stocks != null && Stocks.Count > 0)
                    Stocks[0].Quantity = value;
            }
        }

        public Product(int id) : this()
        {
            Id = id;
        }
        public Product()
        {

        }
        public Product(string name, Company? company, int? reorderThreshold, Category? category, List<Supplier> suppliers, List<Stock> stocks) : this()
        {
            Name = name;
            Company = company;
            ReorderThreshold = reorderThreshold;
            Category = category;
            Suppliers = suppliers;
            Stocks = stocks;
        }
        public Product(List<object> args)
        {
            Id = (int)args[0];
            Name = (string)args[1];
            Company = (Company)args[2];
            ReorderThreshold = (int)args[3];
            Category = (Category)args[4];
            Suppliers = ((List<Supplier>)args[5]);
            Stocks = new List<Stock>();
            InitialArgs = args;
            InitialArgs.RemoveAt(0);
        }
        public void Save(bool isAdd = false)
        {
            ProductDL.Save(this, isAdd);
        }
    }
}
