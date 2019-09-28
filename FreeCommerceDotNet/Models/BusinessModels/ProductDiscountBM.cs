using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductDiscountBM
    {
        public ProductDiscount ProductDiscount { get; set; }
        public Product Product;
        public Segment Segment { get; set; }

        public ProductDiscountBM()
        {
            ProductDiscount = new ProductDiscount();
            Product=new Product();
            Segment=new Segment();
        }
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
                using (SegmentManager m = new SegmentManager())
                {
                    Segment = m.Get(ProductDiscount.Segment);
                }
                return;
            }
            ProductDiscount=new ProductDiscount();

        }
    }
}