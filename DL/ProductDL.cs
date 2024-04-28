using Microsoft.Data.SqlClient;
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
                                                    WHERE Id="+id.ToString());
            List<object> args = Utils.GetArgs(reader);
            if (args.Count != 0)
             {
                 if (args[2] != null)
                     args[2] = CompanyDL.GetCompany((int)args[2]);
                 if (args[4] != null)
                     args[4] = CategoryDL.GetCategory((int)args[4]);
                args.Add(SupplierDL.GetProductSuppliers((int)args[0]));
                 return new Product(args);
             }
             else
            return null;
        }
        public static void Save(Product product,bool isAdd) 
        {
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(product.Name),product.Name),
                ("CompanyId",product.Company.Id),
                (nameof(product.ReorderThreshold),product.ReorderThreshold),
                ("CategoryId",product.Category.Id),
            };
            if(isAdd==true)
            {
                DataHandler.InsertDataSP(args, "stpInsertProduct");
            }
            else
            {
                args.Add(("UpdatedOn", ("CURRENT_TIMESTAMP", true)));
                DataHandler.UpdateData(args, product.InitialArgs, product.GetType().Name, (nameof(product.Id), product.Id));

            }
        }
        public static void DeleteProduct(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteProduct", ("Id", id));
        }
    }
}
