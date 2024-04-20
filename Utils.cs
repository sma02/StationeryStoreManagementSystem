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
        public static List<object> ConstructObjects(SqlDataReader reader, Type type)
        {
            List<object> args;
            List<object> objs = new List<object>();
            while (reader.Read())
            {
                args = new List<object>();
                var dbColumns = reader.GetColumnSchema();
                for(int i = 0;i < dbColumns.Count; i++)
                {
                    if (reader.GetValue(i).GetType() == typeof(DBNull))
                    {
                        args.Add(null);
                    }
                    else
                    {
                        args.Add(reader.GetValue(i));
                    }
                }
                objs.Add(Activator.CreateInstance(type, args));
            }
            return objs;
        }
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
