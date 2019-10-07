using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class OrderReturnManager:IOrderReturnService
    {
        private IOrderReturnDal _orderReturnDal;

        public OrderReturnManager()
        {
            
        }

        public OrderReturnManager(IOrderReturnDal orderReturnDal)
        {
            _orderReturnDal = orderReturnDal;
        }
        public ServiceResult Insert(OrderReturn entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderReturnDal.Insert(entity));
        }

        public ServiceResult Update(OrderReturn entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderReturnDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderReturnDal.Delete(id));
        }

        public OrderReturn SelectById(int id)
        {
            return _orderReturnDal.SelectById(id);
        }

        public List<OrderReturn> SelectByFilter(List<DBFilter> filters)
        {
            return _orderReturnDal.SelectByFilter(filters);
        }

        public List<OrderReturn> SelectAll()
        {
            return _orderReturnDal.SelectAll();
        }

        public void Dispose()
        {
        }
    }
}