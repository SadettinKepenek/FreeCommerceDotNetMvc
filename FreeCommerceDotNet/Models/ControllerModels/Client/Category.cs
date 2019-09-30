using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace FreeCommerceDotNet.Models.ControllerModels.Client
{
    public class Category
    {
        public CategoryBM CategoryBm { get; set; }
        public List<BrandBM> BrandBms { get; set; } = new List<BrandBM>();
        public List<ProductBM> ProductBms { get; set; }=new List<ProductBM>();


        public Category(int id)
        {
            CategoryBm = new CategoryBM(id);

            foreach (Product product in CategoryBm.Products)
            {
                ProductBms.Add(new ProductBM(product.ProductId));
            }

            foreach (Product bmProduct in CategoryBm.Products)
            {
                BrandBms.Add(new BrandBM(bmProduct.Brand));
            }

            BrandBms = BrandBms.Distinct().ToList();
        }
    }
}