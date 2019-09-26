using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class AttributeGroupBM
    {
        public AttributeGroup AttributeGroup { get; set; }
        public List<Attribute> Attributes { get; set; }

        public AttributeGroupBM()
        {
            AttributeGroup = new AttributeGroup();
            Attributes = new List<Attribute>();
        }
        public AttributeGroupBM(int? id)
        {
            if (id == null)
            {
                AttributeGroup = new AttributeGroup();
                Attributes = new List<Attribute>();
            }
            else
            {
                var key = (int)id;

                using (var m = new AttributeGroupManager())
                {
                    AttributeGroup = m.Get(key);
                }
                using (AttributeManager m = new AttributeManager())
                {
                    this.Attributes = m.GetByIntegerKey(key, "Attributes", "AttributeGroupId");
                   
                }
            }
        }
    }
}