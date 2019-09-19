using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductOptionBM
    {
        public ProductOption ProductOption { get; set; }
        public List<OptionDetail> OptionDetails { get; set; }
        public List<Product> Products { get; set; }

        public ProductOptionBM(int? id)
        {
            if (id == null)
            {
                ProductOption = new ProductOption();
                OptionDetails = new List<OptionDetail>();
                Products = new List<Product>();
            }
            else
            {

                using (var m = new ProductOptionsManager())
                {
                    int key = (int)id;
                    ProductOption = m.Get(key);
                }
                using (var m = new OptionDetailManager())
                {
                    int key = (int)id;
                    OptionDetails = m.GetByIntegerKey(key, "OptionsDetail", "ValueId");
                }
                using (var m = new ProductManager())
                {
                    int key = (int)id;
                    Products = m.GetByIntegerKey(key, "Products", "ProductId");
                }

            }
        }
    }
}