using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class CategoryDL
    {
        public static List<Category> GetCategories()
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id, Name, GST 
                                                    FROM Category C
                                                    JOIN TaxLog T ON C.Id = T.CategoryId ");
            return DataHandler.ConstructObjects(reader, typeof(Category)).Cast<Category>().ToList();
        }
        public static void InsertCategory(Category C)
        {
            string query = $"EXEC stpInsertCategory @Name={C.Name} , @GST = {C.GST}";
            Utils.ExecuteQuery(query);
        }
    }
}
