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
        public static object NormalizeForORM(object value)
        {
            if (value.GetType() == typeof(DBNull))
            {
                return null;
            }
            else
                return value;
        }
        public static List<object> GetArgs(SqlDataReader reader)
        {
            List<object> args = new List<object>();
            if (reader.Read())
            {
                var dbColumns = reader.GetColumnSchema();
                for (int i = 0; i < dbColumns.Count; i++)
                {
                    args.Add(NormalizeForORM(reader.GetValue(i)));
                }
            }
            return args;
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
