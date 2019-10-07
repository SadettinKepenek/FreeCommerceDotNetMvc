using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class ShippingManager:IShippingService
    {
        private readonly IShippingDal _shippingDal;

        public ShippingManager()
        {
            
        }

        public ShippingManager(IShippingDal shippingDal)
        {
            _shippingDal = shippingDal;
        }
        public ServiceResult Insert(Shipping entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_shippingDal.Insert(entity));
        }

        public ServiceResult Update(Shipping entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_shippingDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_shippingDal.Delete(id));
        }

        public Shipping SelectById(int id)
        {
            return _shippingDal.SelectById(id);
        }

        public List<Shipping> SelectByFilter(List<DBFilter> filters)
        {
            return _shippingDal.SelectByFilter(filters);
        }

        public List<Shipping> SelectAll()
        {
            return _shippingDal.SelectAll();
        }
    }
}