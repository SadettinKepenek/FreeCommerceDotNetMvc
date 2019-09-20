using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class OrderDetailBusinessManager:IBusinessOperations<OrderDetailBM>
    {
        public void Dispose()
        {
            
        }

        public List<OrderDetailBM> Get()
        {
            using (OrderDetailManager m = new OrderDetailManager())
            {
                List<OrderDetail> orderDetails = m.GetAll();
                List<OrderDetailBM> businessModels = new List<OrderDetailBM>();
                foreach (var detail in orderDetails)
                {
                    businessModels.Add(new OrderDetailBM(detail.OrderId));
                }

                return businessModels;
            }
        }

        public OrderDetailBM GetById(int id)
        {
            return new OrderDetailBM(id);
        }

        public int Add(OrderDetailBM entry)
        {
            using (OrderDetailManager m = new OrderDetailManager())
            {
                entry.Detail.ProductId = entry.ProductBm.ProductId;
                entry.Detail.OrderId = entry.OrderMaster.OrderId;
                return m.Add(entry.Detail);
            }
        }

        public bool Update(OrderDetailBM entry)
        {
            using (OrderDetailManager m = new OrderDetailManager())
            {
                entry.Detail.ProductId = entry.ProductBm.ProductId;
                entry.Detail.OrderId = entry.OrderMaster.OrderId;
                m.Update(entry.Detail);
                return true;
            }
        }

        public bool Delete(OrderDetailBM entry)
        {
            using (OrderDetailManager m = new OrderDetailManager())
            {
                return m.Delete(entry.Detail.OrderDetailId);
            }
        }
    }
}