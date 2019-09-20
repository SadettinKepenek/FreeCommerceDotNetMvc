using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductOptionBusinessManager:IBusinessOperations<ProductOptionBM>
    {
        public void Dispose()
        {

        }

        public List<ProductOptionBM> Get()
        {
            using (ProductOptionsManager m = new ProductOptionsManager())
            {
                List<ProductOption> dbProducts = m.GetAll();
                List<ProductOptionBM> businessModels = new List<ProductOptionBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new ProductOptionBM(product.ProductId));
                }

                return businessModels;
            }
        }

        public ProductOptionBM GetById(int id)
        {
            using (ProductOptionsManager m = new ProductOptionsManager())
            {
                return new ProductOptionBM(id);
            }
        }

        public int Add(ProductOptionBM entry)
        {
            using (ProductOptionsManager m = new ProductOptionsManager())
            {
                return m.Add(entry.ProductOption);
            }
        }

        public bool Update(ProductOptionBM entry)
        {
            using (ProductOptionsManager m = new ProductOptionsManager())
            {
                m.Update(entry.ProductOption);
                return true;
            }
        }

        public bool Delete(ProductOptionBM entry)
        {
            using (ProductOptionsManager m = new ProductOptionsManager())
            {
                m.Delete(entry.ProductOption.ProductId);
                return true;
            }
        }
    }
}