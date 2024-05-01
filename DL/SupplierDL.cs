using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
            return DataHandler.FillDataTable(@"SELECT * FROM GetSuppliers_View");
        }
        public static Supplier GetSupplier(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Supplier.Id
                                                          ,Name
                                                    	  ,Contact
                                                    	  ,Email
                                                    	  ,StreetAddress
                                                    	  ,Town
                                                    	  ,City
                                                    	  ,Country
                                                    	  ,PostalCode
                                                    FROM Supplier
                                                    WHERE Id=" + id.ToString());
            return (Supplier)DataHandler.ConstructObject(reader, typeof(Supplier));
;        }
        public static List<Supplier> GetProductSuppliers(Product product)
        {
            return GetProductSuppliers(product.Id);
        }
        public static List<Supplier> GetProductSuppliers(int ProductId)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Supplier.Id
                                                          ,Name
                                                          ,Contact
                                                          ,Email
                                                          ,StreetAddress
                                                          ,Town
                                                    	  ,l1.Value City
                                                    	  ,l2.Value Country
                                                          ,PostalCode
                                                    FROM ProductSupplier
													JOIN Lookup l1
													ON l1.Id=City
													JOIN Lookup l2
													ON l2.Id=Country
                                                    JOIN Supplier
                                                    ON Supplier.Id=ProductSupplier.SupplierId
                                                    WHERE ProductSupplier.isDeleted=0 AND ProductId=" + ProductId.ToString());
            return DataHandler.ConstructObjects(reader, typeof(Supplier)).Cast<Supplier>().ToList();
        }
        public static void SaveSupplier(Supplier supplier,bool isAdd)
        {
            int CountryId = DataHandler.LookupData("Country").Where(x => x.Value == supplier.Country).Select(x => x.Key).FirstOrDefault();
            int cityId = DataHandler.LookupData($"City{supplier.Country}").Where(x => x.Value == supplier.City).Select(x => x.Key).FirstOrDefault();
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(supplier.Name), supplier.Name),
                (nameof(supplier.Contact), supplier.Contact),
                (nameof(supplier.Email),supplier.Email),
                (nameof(supplier.StreetAddress),supplier.StreetAddress),
                (nameof(supplier.Town),supplier.Town),
                (nameof(supplier.City),cityId),
                (nameof(supplier.Country),CountryId),
                (nameof(supplier.PostalCode),supplier.PostalCode)
            };
            if (isAdd == true)
            {
                DataHandler.InsertDataSP(args, "stpInsertSupplier");
            }
            else
            {
                args.Add(("UpdatedOn",("CURRENT_TIMESTAMP",true)));
                DataHandler.UpdateData(args, supplier.InitialArgs, supplier.GetType().Name, (nameof(supplier.Id), supplier.Id));
            }
        }
        public static void DeleteSupplier(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteSupplier", ("Id", id));
        }
    }
}
