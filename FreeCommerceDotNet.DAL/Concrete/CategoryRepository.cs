using System;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class CategoryRepository : ICategoryDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Insert(Category entity)
        {
            string query = "CategoryInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@ParentId", entity.ParentId);
                command.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@MetatagTitle", entity.MetatagTitle);
                command.Parameters.AddWithValue("@MetatagDescription", entity.MetatagDescription);
                command.Parameters.AddWithValue("@Metatagkeywords", entity.Metatagkeywords);
                command.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl);
                command.Parameters.AddWithValue("@ShowNavbar", entity.ShowNavbar);
                command.Parameters.AddWithValue("@isActive", entity.isActive);
                DataTable queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }

        public DBResult Update(Category entity)
        {
            string query = "CategoryInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                command.Parameters.AddWithValue("@ParentId", entity.ParentId);
                command.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@MetatagTitle", entity.MetatagTitle);
                command.Parameters.AddWithValue("@MetatagDescription", entity.MetatagDescription);
                command.Parameters.AddWithValue("@Metatagkeywords", entity.Metatagkeywords);
                command.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl);
                command.Parameters.AddWithValue("@ShowNavbar", entity.ShowNavbar);
                command.Parameters.AddWithValue("@isActive", entity.isActive);
                DataTable queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }

        public DBResult Delete(int id)
        {
            string query = "CategoryInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@CategoryId", id);
                DataTable queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }

        public Category SelectById(int id)
        {
            string query = "SP_GetCategory";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                //ayrı olması gerekir sanırım
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryId", id);
                DataTable queryResult = database.DoQuery(command: command);
                
                if (queryResult.Rows.Count != 0)
                {
                    return GetCategory(queryResult);
                }
            }
            return null;
        }

        public List<Category> SelectByFilter(List<DBFilter> filters)
        {

            if (filters.FirstOrDefault(x=>x.ParamName=="@CategoryId")==null)
            {
                throw new ArgumentNullException("@CategoryId","Filtreleme işlemlerinde category id göndermek zorunludur.");
            }

            string query = "SP_GetCategory";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                
                foreach (DBFilter filter in filters)
                {
                    command.Parameters.AddWithValue(filter.ParamName, filter.ParamValue);
                }
                DataTable queryResult = database.DoQuery(command: command);

                if (queryResult.Rows.Count!=0)
                {
                    var selectByFilter = new List<Category>();
                    selectByFilter.Add(GetCategory(queryResult));
                    return selectByFilter;
                }

            }
            return null;
        }

        public List<Category> SelectAll()
        {
            string query = "SP_GetCategory";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                DataTable queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    List<Category> categories=new List<Category>();
                    foreach (DataRow categoryRow in queryResult.Rows)
                    {
                        int categoryId = (int) categoryRow["CategoryId"];
                        if (categories.FirstOrDefault(x=>x.CategoryId==categoryId)==null)
                        {
                            Category category = new Category();
                            category.CategoryId = (int)categoryRow["CategoryId"];
                            category.ParentId = (int)categoryRow["ParentId"];
                            category.CategoryName = categoryRow["CategoryName"].ToString();
                            category.Description = categoryRow["Description"].ToString();
                            category.MetatagTitle = categoryRow["MetatagTitle"].ToString();
                            category.MetatagDescription = categoryRow["MetatagDescription"].ToString();
                            category.Metatagkeywords = categoryRow["Metatagkeywords"].ToString();
                            category.ImageUrl = categoryRow["ImageUrl"].ToString();
                            category.ShowNavbar = (bool)categoryRow["ShowNavbar"];
                            category.isActive = (bool)categoryRow["isActive"];
                            
                            categories.Add(category);

                        }
                        
                    }

                    return categories;
                }
            }

            return null;
        }


        #region Utilities

        private static Category GetCategory(DataTable queryResult)
        {
            var dtRow = queryResult.Rows[0];
            Category category = new Category();

            // Category Bilgileri

            category.CategoryId = (int)dtRow["CategoryId"];
            category.ParentId = (int)dtRow["ParentId"];
            category.CategoryName = dtRow["CategoryName"].ToString();
            category.Description = dtRow["Description"].ToString();
            category.MetatagTitle = dtRow["MetatagTitle"].ToString();
            category.MetatagDescription = dtRow["MetatagDescription"].ToString();
            category.Metatagkeywords = dtRow["Metatagkeywords"].ToString();
            category.ImageUrl = dtRow["ImageUrl"].ToString();
            category.ShowNavbar = (bool)dtRow["ShowNavbar"];
            category.isActive = (bool)dtRow["isActive"];

            var products = GetCategorysProducts(queryResult);
            category.SubCategories = GetCategorysSub(queryResult);
            category.Products = products;
            return category;
        }
        private static List<Product> GetCategorysProducts(DataTable queryResult)
        {
            List<Product> products = new List<Product>();

            // Gelen Dataları product id ye göre gruplar
            var groupedQueryResult = queryResult.AsEnumerable()
                .GroupBy(row => row.Field<int>("ProductId"))
                .Select(grp => grp);
            foreach (IGrouping<int, DataRow> group in groupedQueryResult)
            {
                int productId = @group.Key;
                DataRow mainRow = @group.FirstOrDefault();
                Product product = new Product();
                List<ProductDiscount> productDiscounts = new List<ProductDiscount>();
                List<ProductPrice> productPrices = new List<ProductPrice>();
                product.ProductName = mainRow["ProductName"] as string;
                Debug.WriteLine(product.ProductName);
                product.ProductId = productId;
                product.Quantity = (int)mainRow["ProductQuantity"];
                product.ProductCode = mainRow["ProductCode"] as string;
                product.ImageUrl = mainRow["ProductImageUrl"] as string;
                product.Image1Url = mainRow["ProductImage1Url"] as string;
                product.OutofStockStatus = mainRow["ProductOutOfStockStatus"] as string;
                product.Status = (bool)mainRow["ProductStatus"];
                int brandId = (int)mainRow["BrandId"];
                product.Brand = brandId;
                product.brand = new Brand()
                {
                    BrandName = mainRow["BrandName"] as string,
                    BrandId = brandId
                };

                foreach (DataRow productsRow in @group)
                {
                    int segmentId = (int)productsRow["SegmentId"];


                    #region ProductsDiscounts

                    if (productsRow["DiscountId"] != DBNull.Value)
                    {
                        int discountId = (int)productsRow["DiscountId"];
                        if (productDiscounts.FirstOrDefault(x => x.DiscountId == discountId) == null)
                        {
                            ProductDiscount discount = new ProductDiscount();
                            discount.DiscountId = discountId;
                            discount.StartDate = productsRow["DiscountStartDate"] as string;
                            discount.EndDate = productsRow["DiscountEndDate"] as string;
                            discount.Quantity = (int)productsRow["DiscountQuantity"];
                            discount.SegmentId = segmentId;
                            discount.ProductId = productId;
                            productDiscounts.Add(discount);
                        }
                    }

                    #endregion

                    #region ProductPrices

                    if (productsRow["PriceId"] != null)
                    {
                        int priceId = (int)productsRow["PriceId"];
                        if (productPrices.FirstOrDefault(x => x.PriceId == priceId) == null)
                        {
                            ProductPrice price = new ProductPrice();
                            price.PriceId = priceId;
                            price.ProductId = productId;
                            price.SegmentId = segmentId;
                            price.Price = Convert.ToDouble(productsRow["ProductPrice"]);
                            productPrices.Add(price);
                        }
                    }

                    #endregion
                }

                product.ProductDiscounts = productDiscounts;
                product.ProductPrices = productPrices;
                products.Add(product);
            }

            return products;
        }
        private static List<Category> GetCategorysSub(DataTable queryResult)
        {
            List<Category> subCategories=new List<Category>();
            foreach (DataRow subRow in queryResult.Rows)
            {
                if (subRow["SubCategoryId"]!=DBNull.Value)
                {
                    int subCategoryId = (int) subRow["SubCategoryId"];
                    if (subCategories.FirstOrDefault(x=>x.CategoryId==subCategoryId)==null)
                    {
                        Category sub=new Category();
                        sub.CategoryId = (int)subRow["SubCategoryId"];
                        sub.ParentId = (int)subRow["CategoryId"];
                        sub.CategoryName = subRow["SubCategoryName"].ToString();
                        sub.Description = subRow["SubCategoryDescription"].ToString();
                        sub.MetatagTitle = subRow["SubCategoryMetaTitle"].ToString();
                        sub.MetatagDescription = subRow["SubCategoryMetaDescription"].ToString();
                        sub.Metatagkeywords = subRow["SubCategoryMetaKeywords"].ToString();
                        sub.ImageUrl = subRow["SubCategoryImageUrl"].ToString();
                        sub.ShowNavbar = (bool)subRow["SubCategoryShowNavbar"];
                        sub.isActive = (bool)subRow["SubCategoryIsActive"];
                        subCategories.Add(sub);
                    }
                }

            }

            return subCategories;
            
        }

        #endregion
    }
}