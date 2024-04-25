using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                    args.Add(Utils.NormalizeForORM(reader.GetValue(i)));
                }
                objs.Add(Activator.CreateInstance(type, args));
            }
            return objs;
        }
        public static object ConstructObject(SqlDataReader reader, Type type)
        {
            List<object> args;
            object obj;
            if (reader.Read())
            {
                args = new List<object>();
                var dbColumns = reader.GetColumnSchema();
                for (int i = 0; i < dbColumns.Count; i++)
                {
                    args.Add(Utils.NormalizeForORM(reader.GetValue(i)));
                }
                return Activator.CreateInstance(type, args);
            }
            else
                return null;
        }
        public static DataTable FillDataTable(string query)
        {
            Utils.CloseReader();
            var conn = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
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
            for (int i = 0; i < initialArgs.Count; i++)
            {
                (string, object) struc = args[i];
                if (struc.Item2?.ToString() != initialArgs[i]?.ToString())
                {
                    updatedAttributes.Add(struc.Item1 + " = " + Utils.NormalizeForQuery(struc.Item2));
                }
            }
            for (int i = initialArgs.Count; i < args.Count; i++)
            {
                (string, object) struc = args[i];
                updatedAttributes.Add(struc.Item1 + " = " + Utils.NormalizeForQuery(struc.Item2));

            }
            if (updatedAttributes.Count != 0)
            {
                Utils.ExecuteQuery($"UPDATE {relation} SET {string.Join(',', updatedAttributes)} WHERE {id.Item1}={id.Item2}");

            }
        }
        public static void InsertDataSP(List<(string, object)> args, string stpName)
        {
            List<string> adjustedAttributes = new List<string>();
            for (int i = 0; i < args.Count; i++)
            {
                (string, object) struc = args[i];
                adjustedAttributes.Add("@" + struc.Item1 + " = " + Utils.NormalizeForQuery(struc.Item2));
            }
            if (adjustedAttributes.Count != 0)
                Utils.ExecuteQuery($"EXEC  {stpName} {string.Join(',', adjustedAttributes)}");
        }
        public static void DeleteDataSP(string stpName, (string, object) id)
        {
            Utils.ExecuteQuery($"EXEC {stpName} @{id.Item1} = {id.Item2}");
        }
        public static void InsertData(List<(string, object)> args, string relationName)
        {
            List<string> adjustedAttributes = new List<string>();
            for (int i = 0; i < args.Count; i++)
            {
                (string, object) struc = args[i];
                adjustedAttributes.Add(Utils.NormalizeForQuery(struc.Item2).ToString());
            }
            if (adjustedAttributes.Count != 0)
                Utils.ExecuteQuery($"INSERT INTO {relationName} VALUES ({string.Join(',', adjustedAttributes)}) ");
        }
    }
}

