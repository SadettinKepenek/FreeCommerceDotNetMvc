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
    public class OptionMasterManager : IDBOperations<OptionMaster>, IDisposable
    {
        public int Add(OptionMaster entry)
        {
            using (SqlCommand command = new SqlCommand("sp_optionmaster_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<OptionMaster>(sqlCommand);
            }

        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("OptionsMaster","OptionId",id);
            
        }

        public List<OptionMaster> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<OptionMaster>(id, tbl, key);

        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM OptionsMaster WHERE OptionId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<OptionMaster>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
        }

        public OptionMaster Get(int id)
        {
            string sqlQuery = "select * from OptionsMaster OptionId = @id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var stores = new List<OptionMaster>();
                Utilities.ExecuteCommand<OptionMaster>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return stores.First();
            }
        }

        public List<OptionMaster> GetAll()
        {
            string sqlQuery = "select * from OptionsMaster";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var stores = new List<OptionMaster>();
                Utilities.ExecuteCommand<OptionMaster>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return stores;
            }
        }

        public int Update(OptionMaster entry)
        {
            using (SqlCommand command = new SqlCommand("sp_optionmaster_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OptionMaster>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}