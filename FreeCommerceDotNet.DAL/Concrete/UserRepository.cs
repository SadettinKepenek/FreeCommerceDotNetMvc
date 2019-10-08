using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class UserRepository : IUserDal
    {
        MsSQLDatabase database = new MsSQLDatabase();

        public DBResult Insert(User entity)
        {
            string query = "UserInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@Username", entity.Username);
                command.Parameters.AddWithValue("@Password", entity.Password);
                command.Parameters.AddWithValue("@EMail", entity.EMail);
                command.Parameters.AddWithValue("@Role", entity.Role);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);
                }
            }

            return null;
        }
        public DBResult Update(User entity)
        {
            string query = "UserInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@Username", entity.Username);
                command.Parameters.AddWithValue("@UserId", entity.UserId);
                command.Parameters.AddWithValue("@Password", entity.Password);
                command.Parameters.AddWithValue("@EMail", entity.EMail);
                command.Parameters.AddWithValue("@Role", entity.Role);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);
                }
            }
            return null;
        }
        public DBResult Delete(int id)
        {
            string query = "UserInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@UserId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);
                }
            }
            return null;
        }

        public List<User> SelectAll()
        {
            string query = "SP_GetUser";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                return GetUsers(datatable);
            }
        }
        public User SelectById(int id)
        {
            string query = "SP_GetUser";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                DataTable datatable = database.DoQuery(command: command);
                var rows = datatable.Rows;

                User user = new User();
                user.UserId = id;
                user.Username = rows[0]["Username"] as string;
                user.Password = rows[0]["Password"] as string;
                user.EMail = rows[0]["Email"] as string;
                user.Role = rows[0]["Role"] as string;
                return user;
            }
        }
        public List<User> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetUser";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName,item.ParamValue);
                }
                DataTable datatable = database.DoQuery(command: command);
                return GetUsers(datatable);
            }
        }

        private List<User> GetUsers(DataTable table)
        {
            var rows = table.Rows;
            if (rows.Count != 0)
            {
                List<User> users = new List<User>();
                foreach (DataRow row in rows)
                {
                    User user = new User();
                    user.UserId = (int)row["UserId"];
                    user.Username = row["Username"] as string;
                    user.Password = row["Password"] as string;
                    user.EMail = row["Email"] as string;
                    user.Role = row["Role"] as string;
                    users.Add(user);
                }
                return users;
            }
            return null;
        }


    }
}
