using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using Attribute = System.Attribute;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class AttributeGroupBusinessManager : IBusinessOperations<AttributeGroupBM>
    {
        public int Add(AttributeGroupBM entry)
        {
            using (AttributeGroupManager manager = new AttributeGroupManager())
            {
                int insertedId = manager.Add(entry.AttributeGroup);
                entry.AttributeGroup.AttributeGroupId = insertedId;

                //using (AttributeManager manager2 = new AttributeManager())
                //{
                //    foreach (var attributes in entry.Attributes)
                //    {
                //        attributes.AttributeId = entry.AttributeGroup.AttributeGroupId;
                //        manager2.Add(attributes);
                //    }
                //}

                return entry.AttributeGroup.AttributeGroupId;
            }
        }

        public bool Delete(AttributeGroupBM entry)
        {
            using (AttributeGroupManager manager = new AttributeGroupManager())
            {
                return manager.Delete(entry.AttributeGroup.AttributeGroupId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<AttributeGroupBM> Get()
        {
            using (AttributeGroupManager manager = new AttributeGroupManager())
            {
                List<AttributeGroup> dbAttributeGroups = manager.GetAll();
                List<AttributeGroupBM> businessModel = new List<AttributeGroupBM>();

                foreach (var attributeGroup in dbAttributeGroups)
                {
                    businessModel.Add(new AttributeGroupBM(attributeGroup.AttributeGroupId));
                }

                return businessModel;
            }
        }

        public AttributeGroupBM GetById(int id)
        {
            return new AttributeGroupBM(id);
        }

        public bool Update(AttributeGroupBM entry)
        {
            try
            {
                using (AttributeGroupManager manager = new AttributeGroupManager())
                {
                    int updatedId = manager.Update(entry.AttributeGroup);
                    entry.AttributeGroup.AttributeGroupId = updatedId;
                    using (AttributeManager manager2 = new AttributeManager())
                    {
                        foreach (var attributes in entry.Attributes)
                        {
                            attributes.AttributeGroup = entry.AttributeGroup.AttributeGroupId;
                            manager2.Update(attributes);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}