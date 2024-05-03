using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem
{
    static class Utils
    {
        private static SqlDataReader reader;
        public static MainWindow? CurrentMainWindow { get; set; }
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
        public static List<object> ReaderToList(SqlDataReader reader)
        {
            List<object> objs = new List<object>();
            while (reader.Read())
            {
                List<object> args = new List<object>();
                    var dbColumns = reader.GetColumnSchema();
                    for (int i = 0; i < dbColumns.Count; i++)
                    {
                        args.Add(NormalizeForORM(reader.GetValue(i)));
                    }
                objs.Add(args);
            }
            return objs;
        }
        public static void CloseReader()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
        }
        public static T ToObject<T>(this DataRow dataRow)
    where T : new()
        {
            T item = new T();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                PropertyInfo property = GetProperty(typeof(T), column.ColumnName);

                if (property != null && dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
                {
                    property.SetValue(item, ChangeType(dataRow[column], property.PropertyType), null);
                }
            }

            return item;
        }

        private static PropertyInfo GetProperty(Type type, string attributeName)
        {
            PropertyInfo property = type.GetProperty(attributeName);

            if (property != null)
            {
                return property;
            }

            return type.GetProperties()
                 .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name == attributeName)
                 .FirstOrDefault();
        }

        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
            }

            return Convert.ChangeType(value, type);
        }
    }
}
