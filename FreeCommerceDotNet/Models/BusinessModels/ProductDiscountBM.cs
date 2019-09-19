using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductDiscountBM
    {
        public ProductDiscount ProductDiscount { get; set; }
        public Product Product;
        public ProductDiscountBM(int? id)
        {
            if (id != null)
            {
                using (ProductDiscountManager m = new ProductDiscountManager())
                {
                    ProductDiscount = m.Get((int) id);
                }

                using (ProductManager m = new ProductManager())
                {
                    Product = m.Get(ProductDiscount.ProductId);
                }
                return;
            }
            ProductDiscount=new ProductDiscount();

        }
    }
}