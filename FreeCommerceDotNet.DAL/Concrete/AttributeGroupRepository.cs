using System.Collections.Generic;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class AttributeGroupRepository:IAttributeGroupDal
    {
        public int Insert(AttributeGroup entity)
        {
            return 0;
        }

        public int Update(AttributeGroup entity)
        {
            return 0;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public AttributeGroup SelectById(int id)
        {
            return null;
        }

        public List<AttributeGroup> SelectAll()
        {
            return null;
        }
    }
}