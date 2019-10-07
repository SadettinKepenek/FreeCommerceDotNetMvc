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
    public class AttributeManager : IAttributeService
    {
        private IAttributeDal _attributeRepository;
        public AttributeManager() { }

        public AttributeManager(IAttributeDal attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public ServiceResult Insert(Entities.Concrete.Attribute entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeRepository.Insert(entity));
        }
        public ServiceResult Update(Entities.Concrete.Attribute entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeRepository.Delete(id));

        }


        public List<Entities.Concrete.Attribute> SelectAll()
        {
            return _attributeRepository.SelectAll();

        }

        public List<Entities.Concrete.Attribute> SelectByFilter(List<DBFilter> filters)
        {
            return _attributeRepository.SelectByFilter(filters);
        }

        public Entities.Concrete.Attribute SelectById(int id)
        {
            return _attributeRepository.SelectById(id);
        }

        public void Dispose()
        {
        }
    }
}
