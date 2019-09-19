using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductPriceBM
    {
        public ProductPrice ProductPrice { get; set; }
        public Product Product { get; set; }

        public ProductPriceBM(int? id)
        {
            if (id == null)
            {
                ProductPrice = new ProductPrice();

            }
            else
            {
                using (var m = new ProductPriceManager())
                {
                    int key = (int)id;
                    ProductPrice = m.Get(key);
                }
                using (ProductManager m = new ProductManager())
                {
                    Product = m.Get(ProductPrice.ProductId);
                }
            }
        }

    }
}