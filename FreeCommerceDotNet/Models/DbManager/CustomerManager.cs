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
    public class CustomerManager : IDBOperations<Customer>, IDisposable
    {
        public int Add(Customer entry)
        {
            using (SqlCommand command = new SqlCommand("sp_customers_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Customer>(sqlCommand);
               
            }

        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("Customers", "CustomerId", id);

        }

        public List<Customer> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Customer>(id, tbl, key);

        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Customers WHERE CustomerId=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<Customer>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
        }

        public Customer Get(int id)
        {
            string sqlQuery = "select * from Customers where CustomerId = @id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var customers = new List<Customer>();
                sqlCommand.Parameters.AddWithValue("@id", id);
                Utilities.ExecuteCommand<Customer>(sqlCommand, SqlCommandTypes.Select, ref customers);
                return customers.FirstOrDefault();
            }
        }

        public List<Customer> GetAll()
        {
            string sqlQuery = "select * from Customers";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var customers = new List<Customer>();
                Utilities.ExecuteCommand<Customer>(sqlCommand, SqlCommandTypes.Select, ref customers);
                return customers;
            }
        }

        public int Update(Customer entry)
        {
            using (SqlCommand command = new SqlCommand("sp_customers_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Customer>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}