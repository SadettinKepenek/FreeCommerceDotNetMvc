using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace FreeCommerceDotNet.Models.ControllerModels.Client
{
    public class Category
    {
        public CategoryBM CategoryBm { get; set; }
        public List<Brand> BrandBms { get; set; } = new List<Brand>();
        public List<ProductBM> ProductBms { get; set; }=new List<ProductBM>();
        public int Page { get; set; }


        public Category(int id)
        {
            this.Page = 1;
            CategoryBm = new CategoryBM(id);

            foreach (Product product in CategoryBm.Products.Take(8))
            {
                ProductBms.Add(new ProductBM(product.ProductId));
            }

            foreach (ProductBM bmProduct in ProductBms)
            {
                BrandBms.Add(bmProduct.brand);
            }

            BrandBms = BrandBms.Distinct().ToList();
        }
        public Category(int id,int page)
        {
            this.Page = page;
            CategoryBm = new CategoryBM(id);
            var products = CategoryBm.Products.GetRange(page * 8, (page + 1) * 8);


            foreach (Product product in products)
            {
                ProductBms.Add(new ProductBM(product.ProductId));
            }

            foreach (ProductBM bmProduct in ProductBms)
            {
                BrandBms.Add(bmProduct.brand);
            }

            BrandBms = BrandBms.Distinct().ToList();
        }
    }
}