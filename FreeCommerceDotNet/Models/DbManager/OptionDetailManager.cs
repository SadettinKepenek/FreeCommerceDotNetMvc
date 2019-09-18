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
    public class OptionDetailManager : IOperations<OptionDetail>, IDisposable
    {
        public bool Add(OptionDetail entry)
        {
            using (SqlCommand command = new SqlCommand("sp_optionsdetail_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OptionDetail>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }

        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM OptionsDetail WHERE ValueId=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<OptionDetail>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
        }

        public OptionDetail Get(int id)
        {
            string sqlQuery = "select * from OptionsDetail where ValueId = @id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var stores = new List<OptionDetail>();
                Utilities.ExecuteCommand<OptionDetail>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return stores.First();
            }
        }

        public List<OptionDetail> GetAll()
        {
            string sqlQuery = "select * from OptionsDetail";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var stores = new List<OptionDetail>();
                Utilities.ExecuteCommand<OptionDetail>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return stores;
            }
        }

        public int Update(OptionDetail entry)
        {
            using (SqlCommand command = new SqlCommand("sp_store_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OptionDetail>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}