using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.Interfaces;
using Attribute = FreeCommerceDotNet.Models.DbModels.Attribute;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class AttributeBusinessManager : IBusinessOperations<AttributeBM>
    {
        public int Add(AttributeBM entry)
        {
            using (AttributeManager manager = new AttributeManager())
            {
                entry.Attribute.AttributeGroup = entry.AttributeGroup.AttributeGroupId;
                return manager.Add(entry.Attribute);
            }
        }

        public bool Delete(AttributeBM entry)
        {
            using (AttributeManager manager = new AttributeManager())
            {
                return manager.Delete(entry.Attribute.AttributeId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<AttributeBM> Get()
        {
            using (AttributeManager manager = new AttributeManager())
            {
                List<Attribute> dbAttributes = manager.GetAll();
                List<AttributeBM> businessModels = new List<AttributeBM>();

                foreach (var attributes in dbAttributes)
                {
                    businessModels.Add(new AttributeBM(attributes.AttributeId));
                }

                return businessModels;
            }
        }

        public AttributeBM GetById(int id)
        {
            return new AttributeBM(id);
        }

        public bool Update(AttributeBM entry)
        {
            try
            {
                using (AttributeManager manager = new AttributeManager())
                {
                    manager.Update(entry.Attribute);
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