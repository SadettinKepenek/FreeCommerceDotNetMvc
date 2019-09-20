using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductAttributeBusinessManager:IBusinessOperations<ProductAttributeBM>
    {
        public void Dispose()
        {

        }

        public List<ProductAttributeBM> Get()
        {
            using (ProductAttributeManager m = new ProductAttributeManager())
            {
                List<ProductAttribute> dbProducts = m.GetAll();
                List<ProductAttributeBM> businessModels = new List<ProductAttributeBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new ProductAttributeBM(product.ProductId));
                }

                return businessModels;
            }
        }

        public ProductAttributeBM GetById(int id)
        {
            using (ProductAttributeManager m = new ProductAttributeManager())
            {
                return new ProductAttributeBM(id);
            }
        }

        public int Add(ProductAttributeBM entry)
        {
            using (ProductAttributeManager m = new ProductAttributeManager())
            {
                return m.Add(entry.ProductAttribute);
            }
        }

        public bool Update(ProductAttributeBM entry)
        {
            using (ProductAttributeManager m = new ProductAttributeManager())
            {
                m.Update(entry.ProductAttribute);
                return true;
            }
        }

        public bool Delete(ProductAttributeBM entry)
        {
            using (ProductAttributeManager m = new ProductAttributeManager())
            {
                m.Delete(entry.ProductAttribute.ProductId);
                return true;
            }
        }
    }
}