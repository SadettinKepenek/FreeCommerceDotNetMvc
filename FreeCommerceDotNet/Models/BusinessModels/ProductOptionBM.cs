using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductOptionBM
    {
        public ProductOption ProductOption { get; set; }
        public OptionDetailBM OptionDetails { get; set; }
        public ProductBM Products { get; set; }

        public ProductOptionBM(int? id)
        {
            if (id == null)
            {
                ProductOption = new ProductOption();
                OptionDetails = new OptionDetailBM(null);
                Products = new ProductBM(null);
            }
            else
            {

                using (var m = new ProductOptionsManager())
                {
                    int key = (int)id;
                    ProductOption = m.Get(key);
                    OptionDetails=new OptionDetailBM(ProductOption.ValueId);
                    Products=new ProductBM(ProductOption.ProductId);
                }
               

            }
        }
    }
}