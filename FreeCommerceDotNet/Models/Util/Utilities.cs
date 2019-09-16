using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace FreeCommerceDotNet.Models.Util
{
    public static class Utilities
    {
        public static string connectionString { get; set; } =
            "\"Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;\"";

        public static T FromJson<T>(string jsonObject)
        {

            return new JavaScriptSerializer().Deserialize<T>(jsonObject); ;
        }

        public static string ToJson<T>(T entry)
        {
            var json = new JavaScriptSerializer().Serialize(entry);
            return json;
        }
        public static Product fromProductReader(SqlDataReader readerObject)
        {
            Product p = new Product();
            p.ProductId = (int)readerObject["ProductId"];
            p.CategoryId = (int)readerObject["CategoryId"];
            p.ProductName = readerObject["ProductName"] as string;
            p.ProductDescription = readerObject["ProductDescription"] as string;
            p.MetatagTitle = readerObject["ProductName"] as string;
            p.MetatagDescription = readerObject["ProductName"] as string;
            p.MetatagKeywords = readerObject["MetatagKeywords"] as string;
            p.ProductTags = readerObject["ProductTags"] as string;
            p.ProductCode = readerObject["ProductCode"] as string;
            p.SKU = readerObject["SKU"] as string;
            p.UPC = readerObject["UPC"] as string;
            p.EAN = readerObject["EAN"] as string;
            p.JAN = readerObject["JAN"] as string;
            p.ISBN = readerObject["ISBN"] as string;
            p.MPN = readerObject["MPN"] as string;
            p.Quantity = (int)readerObject["Quantity"];
            p.OutofStockStatus = readerObject["OutOfStockStatus"] as string;
            p.AvailableDate = readerObject["AvailableDate"] as string;
            Debug.WriteLine(readerObject["Length"]);
            p.Length = Convert.ToDouble(readerObject["Length"]);
            p.Weight = Convert.ToDouble(readerObject["Weight"]);
            p.Height = Convert.ToDouble(readerObject["Height"]);
            p.Width = Convert.ToDouble(readerObject["Width"]);
            p.Status = (bool)readerObject["Status"];
            p.Brand = readerObject["Brand"] as string;
            p.ImageUrl = readerObject["ImageUrl"] as string;

            return p;
        }

        public static ProductPrices fromProductPricesReader(SqlDataReader reader)
        {
            ProductPrices productPrices = new ProductPrices();
            productPrices.PriceId = (int)reader["PriceId"];
            productPrices.Product = (int)reader["ProductId"];
            productPrices.Price = Convert.ToDouble(reader["Price"]);
            productPrices.Segment = reader["Segment"].ToString();
            return productPrices;


        }
        public static ProductAttributes fromProductAttributesReader(SqlDataReader reader)
        {
            ProductAttributes productAttributes = new ProductAttributes();

            var attributeGroup = new AttributeGroup()
            {
                AttributeGroupId = (int)reader["AttributeGroupId"],
                AttributeGroupName = reader["AttributeGroupName"].ToString(),
                    
            };
            var attribute = new Attribute()
            {
                AttributeGroup = attributeGroup,
                AttributeId = (int)reader["AttributeId"],
                AttributeGroupId = (int) reader["AttributeGroupId"],
                AttributeName = reader["AttributeName"].ToString(),
            };


            productAttributes.Attribute = attribute;
            productAttributes.RelationId = (int)reader["RelationId"];
            return productAttributes;

        }
    }
}