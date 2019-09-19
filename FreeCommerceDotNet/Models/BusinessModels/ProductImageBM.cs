using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductImageBM
    {
        public ProductImage ProductImage { get; set; }
        public ProductBM ProductBm { get; set; }

        public ProductImageBM(int? relationId)
        {
            if (relationId != null)
            {
                using (ProductImageManager m = new ProductImageManager())
                {
                    ProductImage = m.Get((int) relationId);
                    ProductBm=new ProductBM(ProductImage.ProductId);
                }
                return;

            }
            ProductImage=new ProductImage();
            ProductBm=new ProductBM(null);
        }
    }
}