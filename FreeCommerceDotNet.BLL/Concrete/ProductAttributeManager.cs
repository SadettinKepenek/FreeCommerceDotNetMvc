using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class ProductAttributeManager:IProductAttributeService
    {
        private IProductAttributeDal _attributeDal;

        public ProductAttributeManager()
        {
            
        }

        public ProductAttributeManager(IProductAttributeDal attributeDal)
        {
            _attributeDal = attributeDal;
        }
        public ServiceResult Insert(ProductAttribute entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeDal.Insert(entity));
        }

        public ServiceResult Update(ProductAttribute entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_attributeDal.Delete(id));
        }

        public ProductAttribute SelectById(int id)
        {
            return _attributeDal.SelectById(id);
        }

        public List<ProductAttribute> SelectByFilter(List<DBFilter> filters)
        {
            return _attributeDal.SelectByFilter(filters);
        }

        public List<ProductAttribute> SelectAll()
        {
            return _attributeDal.SelectAll();
        }
    }
}