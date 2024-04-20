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
        public static void ExecuteQuery(string query)
        {
            closeReader();
            var conn = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
        }
       
        public static SqlDataReader ReadData(string query)
        {
            closeReader();
            var conn = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, conn);
            reader = command.ExecuteReader();
            return reader;
        }
        private static void closeReader()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
        }
    }
}
