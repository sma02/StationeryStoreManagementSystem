using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class SupplierDL
    {
        public static List<Supplier> GetSuppliers()
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
                                                    FROM Supplier");
            return DataHandler.ConstructObjects(reader,typeof(Supplier)).Cast<Supplier>().ToList();
        }
    }
}
