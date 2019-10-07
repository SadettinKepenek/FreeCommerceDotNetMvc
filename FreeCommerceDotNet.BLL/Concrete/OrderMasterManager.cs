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
    public class OrderMasterManager : IOrderMasterService
    {
        private IOrderMasterDal _orderMasterRepository;
        public OrderMasterManager() { }

        public OrderMasterManager(IOrderMasterDal orderMasterRepository)
        {
            _orderMasterRepository = orderMasterRepository;
        }

        public ServiceResult Insert(OrderMaster entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderMasterRepository.Insert(entity));
        }
        public ServiceResult Update(OrderMaster entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderMasterRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderMasterRepository.Delete(id));

        }


        public List<OrderMaster> SelectAll()
        {
            return _orderMasterRepository.SelectAll();

        }

        public List<OrderMaster> SelectByFilter(List<DBFilter> filters)
        {
            return _orderMasterRepository.SelectByFilter(filters);
        }

        public OrderMaster SelectById(int id)
        {
            return _orderMasterRepository.SelectById(id);
        }
    }
}
