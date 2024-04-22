using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class CompanyDL
    {
        public static List<Company> GetCompanies()
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id
                                                          ,Name
                                                    FROM Company");
            return DataHandler.ConstructObjects(reader, typeof(Company)).Cast<Company>().ToList();
        }

        public static void InsertCompany(Company C)
        {
            string query = $"EXEC stpInsertCompany @Name = {C.Name}";
            Utils.ExecuteQuery(query);
        }
    }
}
