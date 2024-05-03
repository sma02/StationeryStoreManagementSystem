using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Stock
    {
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public double RetailPrice { get; set; }
        public double DiscountAmount { get; set; }
        public int Quantity { get; set; }

        public Stock(Supplier supplier, Product product, double price, double retailPrice, double discountAmount, int quantity)
        {
            Supplier = supplier;
            Product = product;
            Price = price;
            RetailPrice = retailPrice;
            DiscountAmount = discountAmount;
            Quantity = quantity;
        }
    }
}
