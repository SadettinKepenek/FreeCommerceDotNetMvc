using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.ControllerModels.Client.ClientFilters;
using FreeCommerceDotNet.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using FreeCommerceDotNet.Models.DbManager;

namespace FreeCommerceDotNet.Models.ControllerModels.Client
{
    public class Category
    {
        public CategoryBM CategoryBm { get; set; }
        public List<Brand> BrandBms { get; set; } = new List<Brand>();
        public List<ProductBM> ProductBms { get; set; } = new List<ProductBM>();
        public int Page { get; set; }
        public int MaxPage { get; set; }

        public int PRODUCTCOUNT { get; private set; } = 12;



        public Category(int id, int page, CategoryFilters filters)
        {

            this.Page = page != 0 ? page : 1;
            CategoryBm = new CategoryBM(id);
            List<Product> products;
            IEnumerable<Product> productsList = CategoryBm.Products; 
            if (filters != null)
            {
                if (filters.AltKategoriId != 0)
                {
                    productsList = productsList.Where(x => x.CategoryId == filters.AltKategoriId);
                }

                if (filters.BrandId!=0)
                {
                    productsList= productsList.Where(x => x.Brand == filters.BrandId);
                }

            }
            else
            {
                productsList = CategoryBm.Products.Skip(page * PRODUCTCOUNT).Take(PRODUCTCOUNT).ToList();
            }

            products = productsList.ToList();
            this.MaxPage = (int)CategoryBm.Products.Count / PRODUCTCOUNT;

            foreach (Product product in products)
            {
                ProductBms.Add(new ProductBM(product.ProductId));
            }

            foreach (ProductBM bmProduct in ProductBms)
            {
                if (BrandBms.FirstOrDefault(x => x.BrandName == bmProduct.brand.BrandName) == null)
                {
                    BrandBms.Add(bmProduct.brand);
                }
            }

            BrandBms = BrandBms.Distinct().ToList();
        }
    }
}