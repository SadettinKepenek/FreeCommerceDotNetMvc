using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class CategoryBM
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> SubCategories { get; set; }

        public CategoryBM(int? id)
        {
            if (id == null)
            {
                Category = new Category();
                Products = new List<Product>();
            }
            else
            {
                int key = (int)id;
                using (CategoryManager m = new CategoryManager())
                {
                    Category = m.Get(key);
                    SubCategories = m.GetByIntegerKey(key, "Categories", "ParentId");
                }

                using (ProductManager m = new ProductManager())
                {
                    Products = m.GetByIntegerKey(key, "Products", "CategoryId");
                }

            }
        }
    }
}