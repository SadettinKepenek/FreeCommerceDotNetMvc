using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductImageBM
    {
        public ProductImage ProductImage { get; set; }
        public Product Product;

        public ProductImageBM(int? relationId)
        {
            if (relationId != null)
            {
                using (ProductImageManager m = new ProductImageManager())
                {
                    ProductImage = m.Get((int) relationId);
                }
                using (ProductManager m = new ProductManager())
                {
                    Product = m.Get(ProductImage.ProductId);
                }
                return;

            }
            ProductImage=new ProductImage();
        }
    }
}