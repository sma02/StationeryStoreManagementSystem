using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    internal class DataHandler
    {
        public static List<object> ConstructObjects(SqlDataReader reader, Type type)
        {
            List<object> args;
            List<object> objs = new List<object>();
            while (reader.Read())
            {
                args = new List<object>();
                var dbColumns = reader.GetColumnSchema();
                for (int i = 0; i < dbColumns.Count; i++)
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
        public static void AddData(List<(string, object)> args, string relation)
        {
            string attributes = string.Join(',', args.Select(x => x.Item1));
            string values = string.Join(',', args.Select(x =>
            {
                if (x.Item2 == null)
                    x.Item2 = "NULL";
                else if (x.Item2.GetType() == typeof(string))
                    x.Item2 = $"'{x.Item2}'";
                return x.Item2;
            }));
            Utils.ExecuteQuery($"INSERT INTO {relation}({attributes}) VALUES ({values})");
        }
        public static void UpdateData(List<(string, object)> args, List<object> initialArgs, string relation, (string, object) id)
        {
            List<string> updatedAttributes = new List<string>();
            for (int i = 0; i < args.Count; i++)
            {
                (string, object) struc = args[i];
                if (struc.Item2?.ToString() != initialArgs[i]?.ToString())
                {
                    object value = struc.Item2;
                    if(value==null)
                    {
                        value = "NULL";
                    }
                    else if(value.GetType()==typeof(string))
                    {
                        value = $"'{value}'";
                    }
                    updatedAttributes.Add(struc.Item1 + " = " + value.ToString());
                }
            }
            if (updatedAttributes.Count != 0)
                Utils.ExecuteQuery($"UPDATE {relation} SET {string.Join(',', updatedAttributes)} WHERE {id.Item1}={id.Item2}");
        }
    }
}
