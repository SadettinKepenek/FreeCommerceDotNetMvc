using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductOptionBM
    {
        public ProductOption ProductOption { get; set; }
        public OptionDetail OptionDetails { get; set; }
        public Product Product { get; set; }
        public ProductOptionBM(int? id)
        {
            if (id == null)
            {
                ProductOption = new ProductOption();
            }
            else
            {

                using (var m = new ProductOptionsManager())
                {
                    int key = (int)id;
                    ProductOption = m.Get(key);
                }
                using (ProductManager m = new ProductManager())
                {
                    Product = m.Get(ProductOption.ProductId);
                }
                using (var m = new OptionDetailManager())
                {
                    OptionDetails = m.Get(ProductOption.ValueId);

                }


            }
        }
    }
}