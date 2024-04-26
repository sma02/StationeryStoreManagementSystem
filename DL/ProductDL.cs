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
        public static DataTable GetProductsView()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM Product");
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
                return new Product(args);
            }
            else
                return null;
        }
    }
}
