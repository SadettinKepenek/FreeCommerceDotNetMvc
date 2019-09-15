using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.Models
{
    public class DbManager
    {
        SqlConnection connection = new SqlConnection("Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;");
        public List<Product> GetProducts(int? id)
        {
            string sql = String.Empty;
            SqlCommand command;
            if (id != null)
            {
                sql = "select * from Products where ProductId=@ProductId";
                command = new SqlCommand(sql, connection);
                SqlParameter ProductId = command.Parameters.Add("@ProductId", SqlDbType.Int);
                ProductId.Value = id;

            }
            else
            {
                sql = "select * from Products";
                command = new SqlCommand(sql, connection);
            }
            return executeProductGetCommand(command);

        }

        private List<Product> executeProductGetCommand(SqlCommand command)
        {
            var productsWithParameter = new List<Product>();
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productsWithParameter.Add(Product.fromReader(reader));
            }

            connection.Close();
            return productsWithParameter;
        }
    }
}