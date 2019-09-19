﻿using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class AttributeBM
    {
        public Attribute Attribute { get; set; }
        public AttributeGroupBM AttributeGroup { get; set; }

        public AttributeBM(int? AttributeId)
        {
            if (AttributeId == null)
            {
                Attribute = new Attribute();
                AttributeGroup = new AttributeGroupBM(null);
            }
            else
            {
                int key = (int) AttributeId;
                using (AttributeManager m = new AttributeManager())
                {
                    Attribute = m.Get(key);
                    AttributeGroup=new AttributeGroupBM(Attribute.AttributeGroupId);
                }
               

            }
        }
    }
}