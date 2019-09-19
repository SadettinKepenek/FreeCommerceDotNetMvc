using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class AttributeGroupBM
    {
        public AttributeGroup AttributeGroup { get; set; }
        public List<AttributeBM> Attributes { get; set; }

        public AttributeGroupBM(int? id)
        {
            if (id == null)
            {
                AttributeGroup = new AttributeGroup();
                Attributes = new List<AttributeBM>();
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
                    var dbAttributes = m.GetByIntegerKey(key, "Attributes", "AttributeGroupId");
                    foreach (Attribute attribute in dbAttributes)
                    {
                        Attributes.Add(new AttributeBM(attribute.AttributeId));
                    }
                }
            }
        }
    }
}