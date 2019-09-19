using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductAttributeBM
    {
        public ProductAttribute ProductAttribute { get; set; }
        public ProductBM ProductBm { get; set; }
        public AttributeBM AttributeBm { get; set; }

        public ProductAttributeBM(int? relationId)
        {
            if (relationId != null)
            {
                using (ProductAttributeManager m = new ProductAttributeManager())
                {
                    ProductAttribute = m.Get((int) relationId);
                    ProductBm=new ProductBM(ProductAttribute.ProductId);
                    AttributeBm=new AttributeBM(ProductAttribute.AttributeId);
                }
                return;
            }
            ProductAttribute=new ProductAttribute();
            ProductBm=new ProductBM(null);
            AttributeBm=new AttributeBM(null);
        }
    }
}