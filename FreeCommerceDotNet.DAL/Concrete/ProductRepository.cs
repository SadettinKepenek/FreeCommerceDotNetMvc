using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Attribute = FreeCommerceDotNet.Entities.Concrete.Attribute;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class ProductRepository : IProductDal
    {
        MsSQLDatabase db = new MsSQLDatabase();
        public DBResult Insert(Product entity)
        {
            string query = "ProductInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                cmd.Parameters.AddWithValue("@ProductName", entity.ProductName);
                cmd.Parameters.AddWithValue("@ProductDescription", entity.ProductDescription);
                cmd.Parameters.AddWithValue("@MetatagTitle", entity.MetatagTitle);
                cmd.Parameters.AddWithValue("@MetatagDescription", entity.MetatagDescription);
                cmd.Parameters.AddWithValue("@MetatagKeywords", entity.MetatagKeywords);
                cmd.Parameters.AddWithValue("@ProductTags", entity.ProductTags);
                cmd.Parameters.AddWithValue("@ProductCode", entity.ProductCode);
                cmd.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl);
                cmd.Parameters.AddWithValue("@Image1Url", entity.Image1Url);
                cmd.Parameters.AddWithValue("@Image2Url", entity.Image2Url);
                cmd.Parameters.AddWithValue("@Image3Url", entity.Image3Url);
                cmd.Parameters.AddWithValue("@Image4Url", entity.Image4Url);
                cmd.Parameters.AddWithValue("@SKU", entity.SKU);
                cmd.Parameters.AddWithValue("@UPC", entity.UPC);
                cmd.Parameters.AddWithValue("@EAN", entity.EAN);
                cmd.Parameters.AddWithValue("@JAN", entity.JAN);
                cmd.Parameters.AddWithValue("@ISBN", entity.ISBN);
                cmd.Parameters.AddWithValue("@MPN", entity.MPN);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.Parameters.AddWithValue("@OutOfStockStatus", entity.OutofStockStatus);
                cmd.Parameters.AddWithValue("@AvailableDate", entity.AvailableDate);
                //cmd.Parameters.AddWithValue("@Rate", entity.Rate);
                cmd.Parameters.AddWithValue("@Length", entity.Length);
                cmd.Parameters.AddWithValue("@Width", entity.Width);
                cmd.Parameters.AddWithValue("@Height", entity.Height);
                cmd.Parameters.AddWithValue("@Weight", entity.Weight);
                cmd.Parameters.AddWithValue("@Status", entity.Status);
                cmd.Parameters.AddWithValue("@Brand", entity.Brand);
                DataTable queryResult = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(queryResult);


            }
        }

        public DBResult Update(Product entity)
        {
            string query = "ProductInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                cmd.Parameters.AddWithValue("@ProductName", entity.ProductName);
                cmd.Parameters.AddWithValue("@ProductDescription", entity.ProductDescription);
                cmd.Parameters.AddWithValue("@MetatagTitle", entity.MetatagTitle);
                cmd.Parameters.AddWithValue("@MetatagDescription", entity.MetatagDescription);
                cmd.Parameters.AddWithValue("@MetatagKeywords", entity.MetatagKeywords);
                cmd.Parameters.AddWithValue("@ProductTags", entity.ProductTags);
                cmd.Parameters.AddWithValue("@ProductCode", entity.ProductCode);
                cmd.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl);
                cmd.Parameters.AddWithValue("@Image1Url", entity.Image1Url);
                cmd.Parameters.AddWithValue("@Image2Url", entity.Image2Url);
                cmd.Parameters.AddWithValue("@Image3Url", entity.Image3Url);
                cmd.Parameters.AddWithValue("@Image4Url", entity.Image4Url);
                cmd.Parameters.AddWithValue("@SKU", entity.SKU);
                cmd.Parameters.AddWithValue("@UPC", entity.UPC);
                cmd.Parameters.AddWithValue("@EAN", entity.EAN);
                cmd.Parameters.AddWithValue("@JAN", entity.JAN);
                cmd.Parameters.AddWithValue("@ISBN", entity.ISBN);
                cmd.Parameters.AddWithValue("@MPN", entity.MPN);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.Parameters.AddWithValue("@OutOfStockStatus", entity.OutofStockStatus);
                cmd.Parameters.AddWithValue("@AvailableDate", entity.AvailableDate);
                //cmd.Parameters.AddWithValue("@Rate", entity.Rate);
                cmd.Parameters.AddWithValue("@Length", entity.Length);
                cmd.Parameters.AddWithValue("@Width", entity.Width);
                cmd.Parameters.AddWithValue("@Height", entity.Height);
                cmd.Parameters.AddWithValue("@Weight", entity.Weight);
                cmd.Parameters.AddWithValue("@Status", entity.Status);
                cmd.Parameters.AddWithValue("@Brand", entity.Brand);
                DataTable queryResult = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(queryResult);


            }
        }

        public DBResult Delete(int id)
        {
            string query = "ProductInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", id);
                DataTable queryResult = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(queryResult);
            }
        }

        public Product SelectById(int id)
        {
            string query = "SP_GetProduct";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductId", id);
                DataTable result = db.DoQuery(command: command);
                if (result.Rows.Count != 0)
                {
                    return GetProduct(result, id);
                }
            }
            return null;
        }


        public List<Product> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetProduct";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;

                foreach (DBFilter filter in filters)
                {
                    command.Parameters.AddWithValue(filter.ParamName, filter.ParamValue);
                }
                DataTable result = db.DoQuery(command: command);
                if (result.Rows.Count != 0)
                {
                    return GetProducts(result);
                }
            }
            return null;
        }

        public List<Product> SelectAll()
        {
            string query = "SP_GetProduct";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;

                DataTable result = db.DoQuery(command: command);
                if (result.Rows.Count != 0)
                {
                    return GetProducts(result);
                }
            }
            return null;
        }




        #region Utilities

        private Product GetProduct(DataTable result, int id = 0)
        {
            if (id == 0)
            {
                GetProducts(result);
            }
            var groupedData = LinqHelper.GroupDataTableByField<int>(queryResult: result, fieldName: "ProductId");
            var productsGroup = groupedData.FirstOrDefault(x => x.Key == id);
            if (productsGroup != null)
            {
                var product = CreateProductInstance(productsGroup);
                return product;
            }
            return null;
        }

        private List<Product> GetProducts(DataTable result)
        {
            List<Product> products = new List<Product>();
            var groupedData = LinqHelper.GroupDataTableByField<int>(queryResult: result, fieldName: "ProductId");
            foreach (IGrouping<int, DataRow> group in groupedData)
            {
                Product product = CreateProductInstance(group);
                products.Add(product);
            }


            return products;
        }
        private static Product CreateProductInstance(IGrouping<int, DataRow> productsGroup)
        {
            Product product = new Product();
            int productId = productsGroup.Key;
            var mainrow = productsGroup.FirstOrDefault();
            product.ProductId = productId;
            product.ProductName = mainrow["ProductName"] as string;
            product.ProductDescription = mainrow["ProductDescription"] as string;
            product.MetatagTitle = mainrow["MetatagTitle"] as string;
            product.MetatagDescription = mainrow["MetatagDescription"] as string;
            product.MetatagKeywords = mainrow["MetatagKeywords"] as string;
            product.ProductTags = mainrow["ProductTags"] as string;
            product.ProductCode = mainrow["ProductCode"] as string;
            product.ImageUrl = mainrow["ImageUrl"] as string;
            product.Image1Url = mainrow["Image1Url"] as string;
            product.Image2Url = mainrow["Image2Url"] as string;
            product.Image3Url = mainrow["Image3Url"] as string;
            product.Image4Url = mainrow["Image4Url"] as string;
            product.SKU = mainrow["SKU"] as string;
            product.UPC = mainrow["UPC"] as string;
            product.EAN = mainrow["EAN"] as string;
            product.JAN = mainrow["JAN"] as string;
            product.ISBN = mainrow["ISBN"] as string;
            product.MPN = mainrow["MPN"] as string;
            product.Quantity = (int)mainrow["Quantity"];
            product.OutofStockStatus = mainrow["OutOfStockStatus"] as string;
            product.AvailableDate = mainrow["AvailableDate"] as string;
            product.Length = Convert.ToDouble(mainrow["Length"]);
            product.Width = Convert.ToDouble(mainrow["Width"]);
            product.Height = Convert.ToDouble(mainrow["Height"]);
            product.Weight = Convert.ToDouble(mainrow["Weight"]);
            product.Status = (bool)mainrow["Status"];
            product.Brand = (int)mainrow["Brand"];
            product.brand = new Brand()
            {
                BrandId = (int)mainrow["Brand"],
                BrandName = mainrow["BrandName"] as string,
                BrandDescription = mainrow["BrandDescription"] as string,
                BrandUrl = mainrow["BrandUrl"] as string,
                BrandImageUrl = mainrow["BrandImageUrl"] as string
            };
            product.CategoryId = (int)mainrow["CategoryId"];
            product.Category = new Category()
            {
                CategoryId = (int)mainrow["CategoryId"],
                CategoryName = mainrow["CategoryName"] as string,
                Metatagkeywords = mainrow["CategorySEOKeywords"] as string
            };

            List<ProductDiscount> productDiscounts = new List<ProductDiscount>();
            List<ProductPrice> productPrices = new List<ProductPrice>();
            List<ProductAttribute> productAttributes = new List<ProductAttribute>();
            foreach (DataRow row in productsGroup)
            {
                if (row["PriceId"] != DBNull.Value)
                {
                    int priceId = (int)row["PriceId"];
                    if (productPrices.FirstOrDefault(x => x.PriceId == priceId) == null)
                    {
                        ProductPrice price = new ProductPrice();
                        price.PriceId = priceId;
                        price.ProductId = productId;
                        int priceSegmentId = (int)row["SegmentId"];
                        price.SegmentId = priceSegmentId;
                        price.Segment = new Segment()
                        {
                            SegmentId = priceSegmentId,
                            SegmentName = row["SegmentName"] as string
                        };
                        price.Price = Convert.ToDouble(row["Price"]);
                        productPrices.Add(price);
                    }
                }

                if (row["DiscountId"] != DBNull.Value)
                {
                    int discountId = (int)row["DiscountId"];
                    if (productDiscounts.FirstOrDefault(x => x.DiscountId == discountId) == null)
                    {
                        ProductDiscount discount = new ProductDiscount();
                        discount.DiscountId = discountId;
                        discount.StartDate = row["DiscountStartDate"] as string;
                        discount.EndDate = row["DiscountEndDate"] as string;
                        discount.NewPrice = Convert.ToDouble(row["DiscountNewPrice"]);
                        discount.Quantity = (int)row["DiscountQuantity"];
                        discount.SegmentId = (int)row["SegmentId"];
                        discount.SegmentEntity = new Segment()
                        {
                            SegmentId = discount.SegmentId,
                            SegmentName = row["SegmentName"] as string
                        };
                        discount.ProductId = productId;
                        productDiscounts.Add(discount);
                    }
                }

                if (row["ProductAttributeRelationId"] != DBNull.Value)
                {
                    int relationId = (int)row["ProductAttributeRelationId"];
                    if (productAttributes.FirstOrDefault(x => x.RelationId == relationId) == null)
                    {
                        ProductAttribute productAttribute = new ProductAttribute();
                        productAttribute.ProductId = productId;
                        productAttribute.RelationId = relationId;
                        productAttribute.AttributeId = (int)row["AttributeId"];
                        productAttribute.AttributeDescription = row["ProductAttributeDescription"] as string;
                        productAttribute.AttributeBm = new Attribute()
                        {
                            AttributeId = (int)row["AttributeId"],
                            AttributeName = row["AttributeName"] as string,
                            AttributeGroupId = (int)row["AttributeGroupId"],
                            AttributeGroup = new AttributeGroup()
                            {
                                AttributeGroupId = (int)row["AttributeGroupId"],
                                AttributeGroupName = row["AttributeGroupName"] as string
                            }
                        };
                        productAttributes.Add(productAttribute);
                    }
                }
            }

            product.ProductAttributes = productAttributes;
            product.ProductDiscounts = productDiscounts;
            product.ProductPrices = productPrices;
            return product;
        }

        #endregion
    }
}