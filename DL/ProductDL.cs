﻿using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class ProductDL
    {
        public static DataTable GetProducts_View()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM GetProducts_View");
        }
        public static Product GetProduct(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT Id
                                                        ,Name
                                                        ,CompanyId
                                                        ,ReorderThreshold
                                                        ,CategoryId
                                                    FROM Product
                                                    WHERE Id="+id.ToString());
            List<object> args = Utils.GetArgs(reader);
            if (args.Count != 0)
             {
                 if (args[2] != null)
                     args[2] = CompanyDL.GetCompany((int)args[2]);
                 if (args[4] != null)
                     args[4] = CategoryDL.GetCategory((int)args[4]);
                args.Add(SupplierDL.GetProductSuppliers((int)args[0]));
                 return new Product(args);
             }
             else
            return null;
        }
        public static void Save(Product product,bool isAdd) 
        {
            int? CompanyId = product.Company?.Id;
            int? CategoryId = product.Category?.Id;
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(product.Name),product.Name),
                ("CompanyId",CompanyId),
                (nameof(product.ReorderThreshold),product.ReorderThreshold),
                ("CategoryId",CategoryId),
            };
            if(isAdd==true)
            {
                DataHandler.InsertDataSP(args, "stpInsertProduct");
            }
            else
            {
                List<object> initialArgs = new List<object>(product.InitialArgs);
                initialArgs[1] = ((Company)initialArgs[1])?.Id;
                initialArgs[3] = ((Category)initialArgs[3])?.Id;
                args.Add(("UpdatedOn", ("CURRENT_TIMESTAMP", true)));
                DataHandler.UpdateData(args, initialArgs, product.GetType().Name, (nameof(product.Id), product.Id));

            }
            List<int> newIds = new List<int>();
            List<int> deleteIds = new List<int>();
            newIds = product.Suppliers.Select(x => x.Id).ToList();
            if (product.InitialArgs != null)
            {
                List<int> prevIds = ((List<Supplier>)product.InitialArgs[4]).Select(x => x.Id).ToList();
                deleteIds = prevIds.Except(newIds).ToList();
                newIds = newIds.Except(prevIds).ToList();
            }
            SqlMetaData[] sqlMetas = new SqlMetaData[]
                {
                    new SqlMetaData("SupplierId",SqlDbType.Int),
                    new SqlMetaData("ProductId",SqlDbType.Int)
                };
            if (newIds.Count > 0)
            {
                var valueInsert = newIds.Select(x =>
                {
                    SqlDataRecord record = new SqlDataRecord(sqlMetas);
                    record.SetInt32(0, x);
                    record.SetInt32(1, product.Id);
                    return record;
                });
                DataHandler.BulkDataExecuteSP("ProductSuppliers", "udtt_ProductSuppliers", "stpInsertProductSuppliers", valueInsert);
            }
            if (deleteIds.Count > 0)
            {
                var valueDelete = deleteIds.Select(x =>
                {
                    SqlDataRecord record = new SqlDataRecord(sqlMetas);
                    record.SetInt32(0, x);
                    record.SetInt32(1, product.Id);
                    return record;
                });
                DataHandler.BulkDataExecuteSP("ProductSuppliers", "udtt_ProductSuppliers", "stpDeleteProductSuppliers", valueDelete);
            }
            }
        public static void DeleteProduct(int id)
        {
            DataHandler.DeleteDataSP("stpDeleteProduct", ("Id", id));
        }
    }
}
