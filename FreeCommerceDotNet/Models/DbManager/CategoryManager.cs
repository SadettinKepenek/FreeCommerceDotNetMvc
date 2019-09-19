﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class CategoryManager:IDBOperations<Category>, IDisposable
    {
        public List<Category> GetAll()
        {
            string sqlQuery = "select * from Categories";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Categories = new List<Category>();
                Utilities.ExecuteCommand<Category>(sqlCommand, SqlCommandTypes.Select, ref Categories);
                return Categories;
            }
        }

        public Category Get(int id)
        {
            string sqlQuery = "select * from Categories where CategoryId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Categories = new List<Category>();
                Utilities.ExecuteCommand<Category>(sqlCommand, SqlCommandTypes.Select, ref Categories);
                return Categories.First();
            }
        }

        public int Add(Category entry)
        {
            using (SqlCommand command = new SqlCommand("sp_categories_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Payment>(sqlCommand);
            }
        }

        public int Update(Category entry)
        {
            using (SqlCommand command = new SqlCommand("sp_categories_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Payment>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Categories WHERE CategoryId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<Payment>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            string sqlQuery = "SELECT COUNT(1) FROM Categories WHERE CategoryId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (command)
                    {
                        try
                        {
                            var reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                int c = (int)reader[0];
                                return c != 0;
                            }

                        }
                        catch (Exception e)
                        {
                            return false;
                        }

                    }
                }
            }
            return true;
        }

        public List<Category> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Category>(id, tbl, key);

        }

        public void Dispose()
        {
        }
    }
}