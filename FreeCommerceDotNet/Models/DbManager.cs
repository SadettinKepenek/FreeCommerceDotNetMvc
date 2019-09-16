using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

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
        public bool AddProduct(Product p)
        {
            using (SqlConnection connection = new SqlConnection("Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;"))
            {
                String query = "INSERT INTO Products (id,username,password,email) VALUES (@id,@username,@password, @email)";

                using (SqlCommand command = new SqlCommand("SP_Product_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CategoryId", p.CategoryId);
                    command.Parameters.AddWithValue("@ProductName", p.ProductName);
                    command.Parameters.AddWithValue("@ProductDescription", p.ProductDescription);
                    command.Parameters.AddWithValue("@MetatagTitle", p.MetatagTitle);
                    command.Parameters.AddWithValue("@MetatagDescription", p.MetatagDescription);
                    command.Parameters.AddWithValue("@MetatagKeywords", p.MetatagKeywords);
                    command.Parameters.AddWithValue("@ProductTags", p.ProductTags);
                    command.Parameters.AddWithValue("@ProductCode", p.ProductCode);
                    command.Parameters.AddWithValue("@SKU", p.SKU);
                    command.Parameters.AddWithValue("@UPC", p.UPC);
                    command.Parameters.AddWithValue("@EAN", p.EAN);
                    command.Parameters.AddWithValue("@JAN", p.JAN);
                    command.Parameters.AddWithValue("@ISBN", p.ISBN);
                    command.Parameters.AddWithValue("@MPN", p.MPN);
                    command.Parameters.AddWithValue("@Quantity", p.Quantity);
                    command.Parameters.AddWithValue("@OutOfStockStatus", p.OutofStockStatus);
                    command.Parameters.AddWithValue("@AvailableDate", p.AvailableDate);
                    command.Parameters.AddWithValue("@Length", p.Length);
                    command.Parameters.AddWithValue("@Width", p.Width);
                    command.Parameters.AddWithValue("@Height", p.Height);
                    command.Parameters.AddWithValue("@Weight", p.Weight);
                    command.Parameters.AddWithValue("@Status", p.Status);
                    command.Parameters.AddWithValue("@Brand", p.Brand);
                    command.Parameters.AddWithValue("@ImageUrl", p.ImageUrl);


                    connection.Open();
                    int result = command.ExecuteNonQuery();
               
                    Debug.WriteLine("Correct ! "+result.ToString());
                }
            }
            return false;
        }
        //Update 
        public List<Product> UpdateProducts(Product p,int id)
        {
            using (SqlConnection connection = new SqlConnection("Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;"))
            {
                
                string query = "UPDATE Products SET ProductName = @ProductName ,CategoryId = @CategoryId,ProductDescription = @ProductDescription , MetatagTitle = @MetatagTitle," +
                    "MetatagDescription = @MetatagDescription, MetatagKeywords = @MetatagKeywords ,ProductTags = @ProductTags,ProductCode = @ProductCode," +
                    "SKU = @SKU,UPC = @UPC,EAN = @EAN,JAN=@JAN,ISBN = @ISBN,MPN = @MPN,Quantity = @Quantity,OutofStockStatus = @OutofStockStatus,AvailableDate = @AvailableDate," +
                    "Length = @Length,Width = @Width,Height = @Height,Weight = @Weight,Status = @Status,Brand = @Brand,ImageUrl = @ImageUrl WHERE ProductId = '"+ id +"' ";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", p.CategoryId);
                    command.Parameters.AddWithValue("@ProductName", p.ProductName);
                    command.Parameters.AddWithValue("@ProductDescription", p.ProductDescription);
                    command.Parameters.AddWithValue("@MetatagTitle", p.MetatagTitle);
                    command.Parameters.AddWithValue("@MetatagDescription", p.MetatagDescription);
                    command.Parameters.AddWithValue("@MetatagKeywords", p.MetatagKeywords);
                    command.Parameters.AddWithValue("@ProductTags", p.ProductTags);
                    command.Parameters.AddWithValue("@ProductCode", p.ProductCode);
                    command.Parameters.AddWithValue("@SKU", p.SKU);
                    command.Parameters.AddWithValue("@UPC", p.UPC);
                    command.Parameters.AddWithValue("@EAN", p.EAN);
                    command.Parameters.AddWithValue("@JAN", p.JAN);
                    command.Parameters.AddWithValue("@ISBN", p.ISBN);
                    command.Parameters.AddWithValue("@MPN", p.MPN);
                    command.Parameters.AddWithValue("@Quantity", p.Quantity);
                    command.Parameters.AddWithValue("@OutOfStockStatus", p.OutofStockStatus);
                    command.Parameters.AddWithValue("@AvailableDate", p.AvailableDate);
                    command.Parameters.AddWithValue("@Length", p.Length);
                    command.Parameters.AddWithValue("@Width", p.Width);
                    command.Parameters.AddWithValue("@Height", p.Height);
                    command.Parameters.AddWithValue("@Weight", p.Weight);
                    command.Parameters.AddWithValue("@Status", p.Status);
                    command.Parameters.AddWithValue("@Brand", p.Brand);
                    command.Parameters.AddWithValue("@ImageUrl", p.ImageUrl);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    Debug.WriteLine("Correct ! " + result.ToString());
                    return executeProductGetCommand(command);
                }

            }
            
        }

        public bool deleteProduct(int id)
        {
            try
            {
                var sql = "delete from Products where ProductId=@ProductId";
                var command = new SqlCommand(sql, connection);
                SqlParameter ProductId = command.Parameters.Add("@ProductId", SqlDbType.Int);
                ProductId.Value = id;
                connection.Open();
                var reader = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Sql Error on Delete" + e.StackTrace);
            }
            
            return true;
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