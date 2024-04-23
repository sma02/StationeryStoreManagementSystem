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
    static class CompanyDL
    {
        public static DataTable GetCompanies()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM GetCompanies_View");
        }

        public static void InsertCompany(Company C)
        {
            string query = $"EXEC stpInsertCompany @Name = {C.Name}";
            Utils.ExecuteQuery(query);
        }

        public static Company GetSupplier(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id, Name
                                                    FROM Company
                                                    WHERE Id=" + id.ToString());
            return (Company)DataHandler.ConstructObject(reader, typeof(Company));
        }
        public static void DeleteCompany(int id) 
        {
            string query = $"EXEC stpDeleteCompany @Id = {id}";
            Utils.ExecuteQuery(query);
        }
    }
}
