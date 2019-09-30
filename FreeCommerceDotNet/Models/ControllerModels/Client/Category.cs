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
        public int MaxPage { get; set; }

        public int PRODUCTCOUNT { get; private set; } = 12;


        
        public Category(int id,int page)
        {
            
            this.Page = page!=0 ? page:1;
            CategoryBm = new CategoryBM(id);

            this.MaxPage =(int) CategoryBm.Products.Count / PRODUCTCOUNT;


            var products = CategoryBm.Products.Skip(page * PRODUCTCOUNT).Take(PRODUCTCOUNT).ToList();

            foreach (Product product in products)
            {
                ProductBms.Add(new ProductBM(product.ProductId));
            }

            foreach (ProductBM bmProduct in ProductBms)
            {
                if (BrandBms.FirstOrDefault(x=>x.BrandName==bmProduct.brand.BrandName)==null)
                {
                    BrandBms.Add(bmProduct.brand);
                }
            }

            BrandBms = BrandBms.Distinct().ToList();
        }
    }
}