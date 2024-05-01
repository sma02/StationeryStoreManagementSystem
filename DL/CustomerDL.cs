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
    static class CustomerDL
    {
        public static DataTable GetCustomersView()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"Select * from GetCustomers_View");
        }

        public static Customer GetCustomer(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT U.Id, U.FirstName, U.LastName, G.Value AS Gender, U.CNIC, U.DateOfBirth, U.Contact, L.Value City, U.Town, U.StreetAddress, U.PostalCode 
                                                    FROM [User] U
                                                    LEFT JOIN Lookup G ON G.Id = U.Gender
                                                    LEFT JOIN Lookup L ON L.Id = U.City
                                                    LEFT JOIN Employee E ON U.Id = E.Id
                                                    WHERE E.Id IS NULL AND U.Id = " + id.ToString());
            return (Customer)DataHandler.ConstructObject(reader, typeof(Customer));
        }

        public static void SaveCustomer(Customer C, bool IsAdd)
        {
            int gender = DataHandler.LookupData("Gender").Where(x => x.Value == C.Gender).Select(X => X.Key).FirstOrDefault();
            int cityId = DataHandler.LookupData("CityPakistan").Where(x => x.Value == C.City).Select(X => X.Key).FirstOrDefault();
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(C.FirstName), C.FirstName),
                (nameof(C.LastName) , C.LastName),
                (nameof(gender), gender),
                (nameof(C.CNIC) , C.CNIC),
                (nameof(C.DateOfBirth) , C.DateOfBirth),
                (nameof(C.Contact) , C.Contact),
                (nameof(C.City),cityId),
                (nameof(C.Town) , C.Town),
                (nameof(C.StreetAddress) , C.StreetAddress),
                (nameof(C.PostalCode) , C.PostalCode),
            };
            if (IsAdd == true)
            {
                DataHandler.InsertDataSP(args, "stpInsertCustomer");
            }
            else
            {
                args.Add(("UpdatedOn", ("CURRENT_TIMESTAMP", true)));
                DataHandler.UpdateData(args, C.InitialArgs, "[User]", (nameof(C.Id), C.Id));
            }
        }
    }
}
