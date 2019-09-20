using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductPriceBusinessManager:IBusinessOperations<ProductPriceBM>
    {
        public void Dispose()
        {
            
        }

        public List<ProductPriceBM> Get()
        {
            using (ProductPriceManager m = new ProductPriceManager())
            {
                List<ProductPrice> dbProducts = m.GetAll();
                List<ProductPriceBM> businessModels = new List<ProductPriceBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new ProductPriceBM(product.ProductId));
                }

                return businessModels;
            }
        }

        public ProductPriceBM GetById(int id)
        {
            using (ProductPriceManager m = new ProductPriceManager())
            {
                return new ProductPriceBM(id);
            }
        }

        public int Add(ProductPriceBM entry)
        {
            using (ProductPriceManager m = new ProductPriceManager())
            {
                return m.Add(entry.ProductPrice);
            }
        }

        public bool Update(ProductPriceBM entry)
        {
            using (ProductPriceManager m = new ProductPriceManager())
            {
                m.Update(entry.ProductPrice);
                return true;
            }
        }

        public bool Delete(ProductPriceBM entry)
        {
            using (ProductPriceManager m = new ProductPriceManager())
            {
                m.Delete(entry.ProductPrice.ProductId);
                return true;
            }
        }
    }
}