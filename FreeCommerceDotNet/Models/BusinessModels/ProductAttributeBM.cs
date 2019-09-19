using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductAttributeBM
    {
        public ProductAttribute ProductAttribute { get; set; }
        public Attribute AttributeBm { get; set; }

        public ProductAttributeBM(int? relationId)
        {
            if (relationId != null)
            {
                using (ProductAttributeManager m = new ProductAttributeManager())
                {
                    ProductAttribute = m.Get((int) relationId);
                }

                using (AttributeManager m = new AttributeManager())
                {
                    AttributeBm =m.Get(ProductAttribute.AttributeId);

                }
                return;
            }
            ProductAttribute=new ProductAttribute();
        }
    }
}