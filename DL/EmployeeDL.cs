using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace StationeryStoreManagementSystem.DL
{
    static class EmployeeDL
    {
        public static DataTable GetEmployeessView()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM GetEmployees_View");
        }

        public static Cashier GetEmployee(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT U.Id, U.FirstName, U.LastName, G.Value AS Gender, U.CNIC, U.DateOfBirth, U.Contact, 
                                                        UA.Email, U.City, U.Town, U.StreetAddress, U.PostalCode, ES.Salary
                                                    FROM UserAccount UA
                                                    JOIN [User] U ON UA.UserId = U.Id
                                                    JOIN Employee E ON U.Id = E.Id
													JOIN EmployeeSalary ES ON E.Id = ES.EmployeeId
                                                    JOIN Lookup RL ON E.Role = RL.Id
                                                    JOIN Lookup SL ON E.Status = SL.Id
                                                    JOIN Lookup G ON U.Gender = G.Id
                                                WHERE U.Id = " + id.ToString());

            return (Cashier)DataHandler.ConstructObject(reader, typeof(Cashier));
        }

        public static void SaveEmployee(Employee E, bool IsAdd)
        {
            KeyValuePair<int, string> gender = DataHandler.LookupData("Gender").Where(x => x.Value == E.Gender.Value.Value).FirstOrDefault();
            int role = DataHandler.LookupData("Role").Where(x => x.Value == E.DetermineRole(E)).Select(X => X.Key).FirstOrDefault();
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(E.FirstName), E.FirstName),
                (nameof(E.LastName), E.LastName),
                (nameof(E.Gender), gender.Value),
                (nameof(E.CNIC), E.CNIC),
                (nameof(E.DateOfBirth), E.DateOfBirth),
                (nameof(E.Contact), E.Contact),
                (nameof(E.City),E.City),
                (nameof(E.Town),E.Town),
                (nameof(E.StreetAddress),E.StreetAddress),
                (nameof(E.PostalCode),E.PostalCode),
            };
            if (IsAdd == true)
            {
                args.Add((nameof(E.Username), E.Username));
                args.Add((nameof(E.Email), E.Email));
                args.Add((nameof(E.Password),E.Password));
                args.Add(("Role", role));
                args.Add((nameof(E.Salary), E.Salary));
                DataHandler.InsertDataSP(args, "stpInsertEmployee");
            }
            else
            {
                E.InitialArgs.RemoveAt(11);
                E.InitialArgs.RemoveAt(7);
                args.Add(("UpdatedOn", ("CURRENT_TIMESTAMP", true)));
                DataHandler.UpdateData(args, E.InitialArgs, "[User]", (nameof(E.Id), E.Id));
            }
        }
        public static void DeleteEmployee(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteEmployee", (nameof(id), id));
        }
    }
}
