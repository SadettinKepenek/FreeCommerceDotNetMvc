using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.DbManager
{

    public class ProductManager : IOperations<Product>, IDisposable
    {
        SqlConnection connection = new SqlConnection("Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;");

        public List<Product> GetAll()
        {
            string sql = String.Empty;
            SqlCommand command;

            sql = "select * from Products";
            command = new SqlCommand(sql, connection);

            #region Execute

            var products = new List<Product>();
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product p = Utilities.fromReader<Product>(reader);
                products.Add(p);

            }

            #endregion

            return products;
        }
        public Product Get(int id)
        {
            string sql = String.Empty;
            SqlCommand command;

            sql = "select * from Products where ProductId=@ProductId";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ProductId",id);

            #region Execute

            var products = new List<Product>();
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product p = Utilities.fromReader<Product>(reader);
                products.Add(p);

            }

            #endregion

            connection.Close();
            return products.First();
        }
        public bool Add(Product p)
        {
            using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
            {
                String query = "INSERT INTO Products (id,username,password,email) VALUES (@id,@username,@password, @email)";

                using (SqlCommand command = new SqlCommand("SP_Product_Insert", connection))
                {
                    var sqlCommand = command;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand =
                        Utilities.CreateUpdateSqlParameters<Product>(sqlCommand, p, p.GetType().GetProperties());

                    connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    Debug.WriteLine("Correct ! " + result.ToString());
                }
            }
            return false;
        }
        public int Update(Product p)
        {
            using (SqlConnection connection = new SqlConnection("Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;"))
            {

                using (SqlCommand command = new SqlCommand("sp_product_update", connection))
                {
                    // Add params
                    var sqlCommand = command;
                    Utilities.CreateUpdateSqlParameters<Product>(sqlCommand, p,p.GetType().GetProperties());
                    connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    Debug.WriteLine("Correct ! " + result.ToString());
                    return 0;
                }

            }
        }
        public bool Delete(int id)
        {
            try
            {
                var sql = "delete from Products where ProductId=@ProductId";
                var command = new SqlCommand(sql, connection);
                SqlParameter ProductId = command.Parameters.Add("@ProductId", SqlDbType.Int);
                ProductId.Value = id;
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Sql Error on Delete" + e.StackTrace);
            }

            return true;
        }

        public bool CheckIsExist(int id)
        {
            return false;
        }

        public void Dispose()
        {
        }
    }
}