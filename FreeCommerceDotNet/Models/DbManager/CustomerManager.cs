﻿using FreeCommerceDotNet.Models.DbModels;
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
    public class CustomerManager : IOperations<Customer>, IDisposable
    {
        public bool Add(Customer entry)
        {
            using (SqlCommand command = new SqlCommand("sp_customers_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Customer>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }

        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("Customers", "CustomerId", id);

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
                Utilities.ExecuteCommand<Customer>(sqlCommand, SqlCommandTypes.Select, ref customers);
                return customers.First();
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