using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class ProductDL
    {
        public static DataTable GetProducts_View()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM GetProducts_View");
        }
        public static Product GetProduct(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id
                                                        ,Name
                                                        ,CompanyId
                                                        ,ReorderThreshold
                                                        ,CategoryId
                                                    FROM Product
                                                    WHERE Id=" + id.ToString());
            List<object> args = Utils.GetArgs(reader);
            if (args.Count != 0)
             {
                 if (args[2] != null)
                     args[2] = CompanyDL.GetCompany((int)args[2]);
                 if (args[4] != null)
                     args[4] = CategoryDL.GetCategory((int)args[4]);
                args.Add(SupplierDL.GetProductSuppliers(id));
                reader = Utils.ReadData(@"SELECT s1.SupplierId,s1.Stock,p1.Price,p1.RetailPrice,p1.DiscountAmount
                                                    FROM (SELECT SupplierId,ProductId,SUM(Stock) Stock
                                                    FROM SupplierStock
                                                    GROUP BY ProductId,SupplierId) s1
                                                    JOIN PriceLog p1
                                                    ON p1.SupplierId=s1.SupplierId AND p1.ProductId=s1.ProductId
                                                    WHERE p1.AddedOn = (SELECT MAX(AddedOn)
                                                    	                FROM PriceLog p2
                                                    	                WHERE p1.SupplierId=p2.SupplierId)
                                                    AND p1.ProductId=" + id.ToString());
                List<Stock> stocks = new List<Stock>();    
                while(reader.Read())
                    {
                        Stock stock = new Stock(((List<Supplier>)args[5]).Where(x => x.Id == reader.GetInt32(0)).FirstOrDefault()
                                               , null
                                               , reader.GetSqlMoney(2).ToDouble()
                                               , reader.GetSqlMoney(3).ToDouble()
                                               , reader.GetSqlMoney(4).ToDouble()
                                               , reader.GetInt32(1));
                    stocks.Add(stock);
                    }
                args.Add(stocks);
                Product product = new Product(args);
                return product;
             }
             else
            return null;
        }
        public static List<Product> GetProducts(List<int> ids)
        {
            List<object> products = new List<object>();
            SqlDataReader reader = Utils.ReadData(@"SELECT Id
                                                        ,Name
                                                        ,CompanyId
                                                        ,ReorderThreshold
                                                        ,CategoryId
                                                    FROM Product
                                                    WHERE Id IN (" + string.Join(',', ids) + ")");
            List<object> args;
            do
            {
                args = Utils.GetArgs(reader);
                if (args.Count != 0)
                    products.Add(args);
            }
            while (args != null && args.Count != 0);
            for (int i = 0; i < products.Count; i++)
            {
                var row = (List<object>)products[i];
                if (row[2] != null)
                    row[2] = CompanyDL.GetCompany((int)row[2]);
                if (row[4] != null)
                    row[4] = CategoryDL.GetCategory((int)row[4]);
                row.Add(new List<Supplier>());
                products[i] = new Product(row);
            }
            return products.Cast<Product>().ToList();
        }
        public static List<Product> GetSupplierProducts(int supplierId,bool populateStock = true)
        {
            List<object> products = new List<object>();
            SqlDataReader reader = Utils.ReadData(@"SELECT ProductId
                                                    	  ,Product.Name
                                                    	  ,Product.CompanyId
                                                          ,Product.ReorderThreshold
                                                    	  ,Product.CategoryId
                                                    FROM ProductSupplier
                                                    JOIN Product
                                                    ON Product.Id=ProductSupplier.ProductId
                                                    WHERE ProductSupplier.isDeleted=0 AND SupplierId=" + supplierId.ToString());
            List<object> args;
            do
            {
                 args = Utils.GetArgs(reader);
                if (args.Count != 0)
                    products.Add(args);
            }
            while (args!=null && args.Count != 0);
            Supplier supplier = null;
            if (populateStock==true)
                 supplier = SupplierDL.GetSupplier(supplierId);
            for(int i=0;i<products.Count;i++)
            {
                var row = (List<object>)products[i];
                if (row[2] != null)
                    row[2] = CompanyDL.GetCompany((int)row[2]);
                if (row[4] != null)
                    row[4] = CategoryDL.GetCategory((int)row[4]);
                row.Add(new List<Supplier>());
                products[i] = new Product(row);
                if(populateStock==true)
                    ((Product)products[i]).Stocks.Add(new Stock(supplier, (Product)products[i], 0, 0, 0, 0));
            }
            return products.Cast<Product>().ToList();
        }
        public static void Save(Product product,bool isAdd) 
        {
            int? CompanyId = product.Company?.Id;
            int? CategoryId = product.Category?.Id;
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(product.Name),product.Name),
                ("CompanyId",CompanyId),
                (nameof(product.ReorderThreshold),product.ReorderThreshold),
                ("CategoryId",CategoryId),
            };
            if(isAdd==true)
            {
                DataHandler.InsertDataSP(args, "stpInsertProduct");
            }
            else
            {
                List<object> initialArgs = new List<object>(product.InitialArgs);
                initialArgs[1] = ((Company)initialArgs[1])?.Id;
                initialArgs[3] = ((Category)initialArgs[3])?.Id;
                args.Add(("UpdatedOn", ("CURRENT_TIMESTAMP", true)));
                DataHandler.UpdateData(args, initialArgs, product.GetType().Name, (nameof(product.Id), product.Id));

            }
            List<int> newIds = new List<int>();
            List<int> deleteIds = new List<int>();
            newIds = product.Suppliers.Select(x => x.Id).ToList();
            if (product.InitialArgs != null)
            {
                List<int> prevIds = ((List<Supplier>)product.InitialArgs[4]).Select(x => x.Id).ToList();
                deleteIds = prevIds.Except(newIds).ToList();
                newIds = newIds.Except(prevIds).ToList();
            }
            SqlMetaData[] sqlMetas = new SqlMetaData[]
                {
                    new SqlMetaData("SupplierId",SqlDbType.Int),
                    new SqlMetaData("ProductId",SqlDbType.Int)
                };
            if (newIds.Count > 0)
            {
                var valueInsert = newIds.Select(x =>
                {
                    SqlDataRecord record = new SqlDataRecord(sqlMetas);
                    record.SetInt32(0, x);
                    record.SetInt32(1, product.Id);
                    return record;
                });
                DataHandler.BulkDataExecuteSP("ProductSuppliers", "udtt_ProductSuppliers", "stpInsertProductSuppliers", valueInsert);
            }
            if (deleteIds.Count > 0)
            {
                var valueDelete = deleteIds.Select(x =>
                {
                    SqlDataRecord record = new SqlDataRecord(sqlMetas);
                    record.SetInt32(0, x);
                    record.SetInt32(1, product.Id);
                    return record;
                });
                DataHandler.BulkDataExecuteSP("ProductSuppliers", "udtt_ProductSuppliers", "stpDeleteProductSuppliers", valueDelete);
            }
            }
        public static void DeleteProduct(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteProduct", ("Id", id));
        }
    }
}
