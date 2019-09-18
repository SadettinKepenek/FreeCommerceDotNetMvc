using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Data;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class StoreManager : IDBOperations<Store>, IDisposable
    {

        public bool Add(Store entry)
        {
            using(SqlCommand command = new SqlCommand("sp_store_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Store>(sqlCommand, SqlCommandTypes.Insert);               
                return true;
            }
            
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Store>(id, tbl, key);
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Store WHERE StoreId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<Store>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
        }

        public Store Get(int id)
        {
            string sqlQuery = "select * from Stores where StoreId = @id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var stores = new List<Store>();
                Utilities.ExecuteCommand<Store>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return stores.First();
            }
        }

        public List<Store> GetAll()
        {
            string sqlQuery = "select * from Stores";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var stores = new List<Store>();
                Utilities.ExecuteCommand<Store>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return stores;
            }
        }

        public int Update(Store entry)
        {
            using (SqlCommand command = new SqlCommand("sp_store_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Store>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}