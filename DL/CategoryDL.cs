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
    static class CategoryDL
    {
        public static DataTable GetCategories()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"Select * from GetCategories_View");
        }
        public static void InsertCategory(Category C)
        {
            string query = $"EXEC stpInsertCategory @Name={C.Name} , @GST = {C.GST}";
            Utils.ExecuteQuery(query);
        }

        public static Category GetSupplier(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id, Name, GST 
                                                    FROM Category C
                                                    JOIN TaxLog T ON C.Id = T.CategoryId
                                                    WHERE Id=" + id.ToString());
            return (Category)DataHandler.ConstructObject(reader, typeof(Category));
        }
        public static void DeleteCategory(int id)
        {
            string query = $"EXEC stpDeleteCategory @Id = {id}";
            Utils.ExecuteQuery(query);
        }
    }
}
