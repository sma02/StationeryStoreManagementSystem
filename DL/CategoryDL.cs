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
        public static void SaveCategory(Category C, bool isAdd = false)
        {
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(C.Name), C.Name)
            };
            if (isAdd == true)
            {
                args.Add((nameof(C.GST), C.GST));
                DataHandler.InsertDataSP(args, "stpInsertCategory");
            }
            else
            {
                DateTime now = DateTime.Now;
                args.Add(("UpdatedOn", now.ToString("yyyy-MM-dd HH:mm:ss")));
                DataHandler.UpdateData(args, C.InitialArgs, C.GetType().Name, (nameof(C.Id), C.Id));
            }
        }

        public static Category GetCategory(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id, Name, GST 
                                                    FROM Category C
                                                    JOIN TaxLog T ON C.Id = T.CategoryId
                                                    WHERE Id=" + id.ToString());
            return (Category)DataHandler.ConstructObject(reader, typeof(Category));
        }
        public static void DeleteCategory(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteCategory", ("Id", id));
        }
    }
}
