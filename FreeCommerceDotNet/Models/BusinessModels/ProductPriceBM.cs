using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductPriceBM
    {
        public ProductPrice ProductPrice { get; set; }
        public ProductBM Products { get; set; }

        public ProductPriceBM(int? id)
        {
            if (id == null)
            {
                ProductPrice = new ProductPrice();
                Products = new ProductBM(null);

            }
            else
            {
                using (var m = new ProductPriceManager())
                {
                    int key = (int)id;
                    ProductPrice = m.Get(key);
                    Products=new ProductBM(ProductPrice.ProductId);
                }
            }
        }

    }
}