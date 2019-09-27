using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Data;
using Attribute = FreeCommerceDotNet.Models.DbModels.Attribute;

namespace FreeCommerceDotNet.Models.DbManager
{
    //System.Attribute ile DbModels.Attribute aynı isme sahip o yüzden hata veriyordu
    public class AttributeManager : IDBOperations<DbModels.Attribute>, IDisposable
    {
        public void Dispose()
        {

        }

        public List<DbModels.Attribute> GetAll()
        {
            string sqlQuery = "select * from Attributes";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var attributes = new List<DbModels.Attribute>();
                Utilities.ExecuteCommand<DbModels.Attribute>(sqlCommand, SqlCommandTypes.Select, ref attributes);
                return attributes;
            }
        }

        public DbModels.Attribute Get(int id)
        {
            string sqlQuery = "select * from Attributes where AttributeId = @id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                var attributes = new List<DbModels.Attribute>();
                Utilities.ExecuteCommand<DbModels.Attribute>(sqlCommand, SqlCommandTypes.Select, ref attributes);
                return attributes.FirstOrDefault();
            }
        }

        public int  Add(DbModels.Attribute entry)
        {
            using (SqlCommand command = new SqlCommand("sp_attribute_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<DbModels.Attribute>(sqlCommand);
            }
        }

        public int Update(DbModels.Attribute entry)
        {
            using (SqlCommand command = new SqlCommand("sp_attribute_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<DbModels.Attribute>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Attributes WHERE AttributeId = @id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<DbModels.Attribute>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public List<Attribute> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Attribute>(id, tbl, key);

        }
    }
}