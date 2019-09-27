using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class AttributeBM
    {
        public Attribute Attribute { get; set; }
        public AttributeGroup AttributeGroup { get; set; }

        public AttributeBM()
        {
            Attribute = new Attribute();
            AttributeGroup = new AttributeGroup();
        }
        public AttributeBM(int? AttributeId)
        {
            if (AttributeId == null)
            {
                Attribute = new Attribute();
                AttributeGroup = new AttributeGroup();
            }
            else
            {
                Attribute=new Attribute();
                AttributeGroup=new AttributeGroup();
                int key = (int) AttributeId;
                using (AttributeManager m = new AttributeManager())
                {
                    Attribute = m.Get(key);
                }

                using (AttributeGroupManager m = new AttributeGroupManager())
                {
                    AttributeGroup = m.Get(Attribute.AttributeGroup);
                }
               

            }
        }
    }
}