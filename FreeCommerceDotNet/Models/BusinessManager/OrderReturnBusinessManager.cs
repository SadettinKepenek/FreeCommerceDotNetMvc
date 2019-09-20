using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class OrderReturnBusinessManager:IBusinessOperations<OrderReturnBM>
    {
        public void Dispose()
        {

        }

        public List<OrderReturnBM> Get()
        {
            using (OrderReturnsManager m = new OrderReturnsManager())
            {
                List<OrderReturn> OrderReturns = m.GetAll();
                List<OrderReturnBM> businessModels = new List<OrderReturnBM>();
                foreach (var detail in OrderReturns)
                {
                    businessModels.Add(new OrderReturnBM(detail.OrderId));
                }

                return businessModels;
            }
        }

        public OrderReturnBM GetById(int id)
        {
            return new OrderReturnBM(id);
        }

        public int Add(OrderReturnBM entry)
        {
            using (OrderReturnsManager m = new OrderReturnsManager())
            {
                entry.OrderReturn.ProductId = entry.ProductBm.ProductId;
                entry.OrderReturn.OrderId = entry.OrderBM.OrderId;
                return m.Add(entry.OrderReturn);
            }
        }

        public bool Update(OrderReturnBM entry)
        {
            using (OrderReturnsManager m = new OrderReturnsManager())
            {
                entry.OrderReturn.ProductId = entry.ProductBm.ProductId;
                entry.OrderReturn.OrderId = entry.OrderBM.OrderId;
                m.Update(entry.OrderReturn);
                return true;
            }
        }

        public bool Delete(OrderReturnBM entry)
        {
            using (OrderReturnsManager m = new OrderReturnsManager())
            {
                return m.Delete(entry.OrderReturn.ReturnId);
            }
        }
    }
}