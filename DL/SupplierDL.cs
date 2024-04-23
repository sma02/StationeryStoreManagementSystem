using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StationeryStoreManagementSystem.DL
{
    static class SupplierDL
    {
        public static DataTable GetSuppliersView()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM GetSuppliers_View");
        }
        public static Supplier GetSupplier(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id
                                                          ,Name
                                                    	  ,Contact
                                                    	  ,Email
                                                    	  ,StreetAddress
                                                    	  ,Town
                                                    	  ,City
                                                    	  ,Country
                                                    	  ,PostalCode
                                                    FROM Supplier
                                                    WHERE Id="+id.ToString());
            return (Supplier)DataHandler.ConstructObject(reader, typeof(Supplier));
;        }
        public static void SaveSupplier(Supplier supplier,bool IsAdd)
        {
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(supplier.Name), supplier.Name),
                (nameof(supplier.Contact), supplier.Contact),
                (nameof(supplier.Email),supplier.Email),
                (nameof(supplier.StreetAddress),supplier.StreetAddress),
                (nameof(supplier.Town),supplier.Town),
                (nameof(supplier.City),supplier.City),
                (nameof(supplier.Country),supplier.Country),
                (nameof(supplier.PostalCode),supplier.PostalCode)
            };
            if (IsAdd == true)
            {
                DataHandler.InsertDataSP(args, "stpInsertSupplier");
            }
            else
            {
                DateTime now = DateTime.Now;
                args.Add(("UpdatedOn", now.ToString()));
                DataHandler.UpdateData(args, supplier.InitialArgs, supplier.GetType().Name, (nameof(supplier.Id), supplier.Id));
            }
        }
        public static void DeleteSupplier(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteSupplier", ("Id", id));
        }
    }
}
