using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductDiscountBM
    {
        public ProductDiscount ProductDiscount { get; set; }
        public ProductBM ProductBm { get; set; }

        public ProductDiscountBM(int? id)
        {
            if (id != null)
            {
                using (ProductDiscountManager m = new ProductDiscountManager())
                {
                    ProductDiscount = m.Get((int) id);
                    ProductBm=new ProductBM(ProductDiscount.ProductId);
                }
                return;
            }
            ProductDiscount=new ProductDiscount();
            ProductBm=new ProductBM(null);

        }
    }
}