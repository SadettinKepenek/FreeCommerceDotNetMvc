using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class UsersManager : IDBOperations<Users>, IDisposable
    {
        public void Dispose()
        {
        }

        public int Add(Users entry)
        {
            using (SqlCommand command = new SqlCommand("sp_users_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<DbModels.Users>(sqlCommand);
            }
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Users WHERE UserId = @id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<DbModels.Users>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public Users Get(int id)
        {
            string query = "select * from Users where UserId = @id";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                var sqlCommand = cmd;
                sqlCommand.Parameters.AddWithValue("@id", id);
                var users = new List<DbModels.Users>();
                Utilities.ExecuteCommand<DbModels.Users>(sqlCommand, SqlCommandTypes.Select, ref users);
                return users.FirstOrDefault();
            }
        }

        public List<Users> GetAll()
        {
            string query = "select * from Users";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                var sqlCommand = cmd;
                var users = new List<DbModels.Users>();
                Utilities.ExecuteCommand<DbModels.Users>(sqlCommand, SqlCommandTypes.Select, ref users);
                return users;
            }
        }

        public List<Users> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Users>(id, tbl, key);
        }

        public int Update(Users entry)
        {
            using (SqlCommand command = new SqlCommand("sp_users_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<DbModels.Users>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}