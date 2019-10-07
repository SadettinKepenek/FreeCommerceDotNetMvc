using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class AttributeGroupManager : IAttributeGroupService
    {
        private IAttributeGroupDal _attributeGroupRepository;
        public AttributeGroupManager() { }

        public AttributeGroupManager(IAttributeGroupDal attributeGroupRepository)
        {
            _attributeGroupRepository = attributeGroupRepository;
        }

        public ServiceResult Insert(AttributeGroup entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeGroupRepository.Insert(entity));
        }
        public ServiceResult Update(AttributeGroup entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeGroupRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeGroupRepository.Delete(id));

        }


        public List<AttributeGroup> SelectAll()
        {
            return _attributeGroupRepository.SelectAll();

        }

        public List<AttributeGroup> SelectByFilter(List<DBFilter> filters)
        {
            return _attributeGroupRepository.SelectByFilter(filters);
        }

        public AttributeGroup SelectById(int id)
        {
            return _attributeGroupRepository.SelectById(id);
        }
    }
}
