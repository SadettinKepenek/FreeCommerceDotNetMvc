using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class ProductOptionsManager:IDBOperations<ProductOption>, IDisposable
    {
        public List<ProductOption> GetAll()
        {
            string sqlQuery = "select * from ProductsOptions";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<ProductOption>();
                Utilities.ExecuteCommand<ProductOption>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public ProductOption Get(int id)
        {
            string sqlQuery = "select * from ProductsOptions where RelationId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<ProductOption>();
                Utilities.ExecuteCommand<ProductOption>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.FirstOrDefault();
            }
        }

        public int Add(ProductOption entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productoption_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<ProductOption>(sqlCommand);
                
            }
        }

        public int Update(ProductOption entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productoption_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductOption>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM ProductsOptions WHERE RelationId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<ProductOption>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("ProductOptions", "RelationId", id);
        }

        public List<ProductOption> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<ProductOption>(id, tbl, key);

        }

        public void Dispose()
        {
        }
    }
}