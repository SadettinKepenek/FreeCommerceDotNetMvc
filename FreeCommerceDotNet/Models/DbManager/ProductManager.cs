using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

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

            return executeProductGetCommand(command);
        }
        public Product Get(int id)
        {
            string sql = String.Empty;
            SqlCommand command;

            sql = "select * from Products where ProductId=@ProductId";
            command = new SqlCommand(sql, connection);
            SqlParameter ProductId = command.Parameters.Add("@ProductId", SqlDbType.Int);
            ProductId.Value = id;


            return executeProductGetCommand(command).First();
        }
        public bool Add(Product p)
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
                    command.Parameters.AddWithValue("@ProductId", p.ProductId);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
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
                var reader = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Sql Error on Delete" + e.StackTrace);
            }

            return true;
        }
        public List<Product> executeProductGetCommand(SqlCommand command)
        {
            var productsWithParameter = new List<Product>();
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product p = Utilities.fromProductReader(reader);
                // Bütün foreign keyleri çektir.
                using (ProductManager productManager = new ProductManager())
                {
                    p.ProductPrices = productManager.GetProductPrices(p.ProductId);
                    p.ProductAttributes = productManager.GetProductAttributes(p.ProductId);
                }

                productsWithParameter.Add(p);

            }

            connection.Close();
            return productsWithParameter;
        }
        public List<ProductPrices> GetProductPrices(int productId)
        {
            string sql = "select * from ProductsPrices where ProductId=@ProductId";
            var command = new SqlCommand(sql, connection);
            SqlParameter ProductId = command.Parameters.Add("@ProductId", SqlDbType.Int);
            ProductId.Value = productId;
            var productPrices = new List<ProductPrices>();
            checkConnection();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productPrices.Add(Utilities.fromProductPricesReader(reader));
            }
            reader.Dispose();
            return productPrices;
        }
        public List<ProductAttributes> GetProductAttributes(int productId)
        {
            string sql = "select Attribute.AttributeId,\r\n       AttributeGroup.AttributeGroupId,\r\n       Attribute.AttributeName,\r\n ProductAttributes.RelationId,\r\n       AttributeGroup.AttributeGroupName\r\nfrom Products as Product\r\n         INNER JOIN ProductsAttributes AS ProductAttributes ON Product.ProductId = ProductAttributes.ProductId\r\n         INNER JOIN Attributes as Attribute on ProductAttributes.AttributeId = Attribute.AttributeId\r\n         INNER JOIN AttributeGroups as AttributeGroup on Attribute.AttributeGroup = AttributeGroup.AttributeGroupId where Product.ProductId=@ProductId";
            var command = new SqlCommand(sql, connection);
            SqlParameter ProductId = command.Parameters.Add("@ProductId", SqlDbType.Int);
            ProductId.Value = productId;
            var productAttributes = new List<ProductAttributes>();
            checkConnection();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var attributes = Utilities.fromProductAttributesReader(reader);

                productAttributes.Add(attributes);
            }
            reader.Dispose();
            return productAttributes;
        }

        private void checkConnection()
        {
            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void Dispose()
        {
        }
    }
}