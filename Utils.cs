using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem
{
    static class Utils
    {
        private static SqlDataReader reader;
        public static object NormalizeForQuery(object value)
        {
            if (value == null)
            {
                value = "NULL";
            }
            else if(value.GetType()==typeof((string,bool)))
            {
                value = (((string, bool))value).Item1;
            }
            else if (value.GetType() == typeof(string))
            {
                value = $"'{value}'";
            }
            return value;
        }
        public static void ExecuteQuery(string query)
        {
            CloseReader();
            var conn = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
        }
       
        public static SqlDataReader ReadData(string query)
        {
            CloseReader();
            var conn = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, conn);
            reader = command.ExecuteReader();
            return reader;
        }
        public static void CloseReader()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
        }
    }
}
