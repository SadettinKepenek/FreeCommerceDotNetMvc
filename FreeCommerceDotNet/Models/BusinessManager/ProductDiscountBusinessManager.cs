using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductDiscountBusinessManager:IBusinessOperations<ProductDiscountBM>
    {
        public void Dispose()
        {

        }

        public List<ProductDiscountBM> Get()
        {
            using (ProductDiscountManager m = new ProductDiscountManager())
            {
                List<ProductDiscount> dbProducts = m.GetAll();
                List<ProductDiscountBM> businessModels = new List<ProductDiscountBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new ProductDiscountBM(product.ProductId));
                }

                return businessModels;
            }
        }

        public ProductDiscountBM GetById(int id)
        {
            using (ProductDiscountManager m = new ProductDiscountManager())
            {
                return new ProductDiscountBM(id);
            }
        }

        public int Add(ProductDiscountBM entry)
        {
            using (ProductDiscountManager m = new ProductDiscountManager())
            {
                return m.Add(entry.ProductDiscount);
            }
        }

        public bool Update(ProductDiscountBM entry)
        {
            using (ProductDiscountManager m = new ProductDiscountManager())
            {
                m.Update(entry.ProductDiscount);
                return true;
            }
        }

        public bool Delete(ProductDiscountBM entry)
        {
            using (ProductDiscountManager m = new ProductDiscountManager())
            {
                m.Delete(entry.ProductDiscount.ProductId);
                return true;
            }
        }
    }
}