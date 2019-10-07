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
    public class OrderDetailManager : IOrderDetailService
    {
        private IOrderDetailDal _orderDetailRepository;
        public OrderDetailManager() { }

        public OrderDetailManager(IOrderDetailDal orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public ServiceResult Insert(OrderDetail entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderDetailRepository.Insert(entity));
        }
        public ServiceResult Update(OrderDetail entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderDetailRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_orderDetailRepository.Delete(id));

        }


        public List<OrderDetail> SelectAll()
        {
            return _orderDetailRepository.SelectAll();

        }

        public List<OrderDetail> SelectByFilter(List<DBFilter> filters)
        {
            return _orderDetailRepository.SelectByFilter(filters);
        }

        public OrderDetail SelectById(int id)
        {
            return _orderDetailRepository.SelectById(id);
        }
    }
}
