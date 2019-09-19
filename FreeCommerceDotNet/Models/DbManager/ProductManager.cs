using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.DbManager
{

    public class ProductManager : IDBOperations<Product>, IDisposable
    {

        public void Dispose()
        {

        }

        public List<Product> GetAll()
        {
            string sqlQuery = "select * from Products";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<Product>();
                Utilities.ExecuteCommand<Product>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public Product Get(int id)
        {
            string sqlQuery = "select * from Products where ProductId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<Product>();
                Utilities.ExecuteCommand<Product>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.FirstOrDefault();
            }
        }

        public int Add(Product entry)
        {
            using (SqlCommand command = new SqlCommand("SP_Product_Insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Product>(sqlCommand);
            }
        }

        public int Update(Product entry)
        {
            using (SqlCommand command = new SqlCommand("sp_product_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Product>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Products WHERE ProductId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<Product>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("Product","ProductId",id);
        }

        public List<Product> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Product>(id, tbl, key);

        }
    }
}