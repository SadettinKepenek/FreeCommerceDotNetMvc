using System;
using System.Collections.Generic;
using System.Linq;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class OrderMasterBusinessManager:IBusinessOperations<OrderMasterBM>
    {
        public void Dispose()
        {
            
        }

        public List<OrderMasterBM> Get()
        {
            using (OrderMasterManager m = new OrderMasterManager())
            {
                List<OrderMaster> orderMasters = m.GetAll();
                List<OrderMasterBM> businessModels = new List<OrderMasterBM>();
                foreach (var master in orderMasters)
                {
                    businessModels.Add(new OrderMasterBM(master.OrderId));
                }

                return businessModels;
            }
        }

        public OrderMasterBM GetById(int id)
        {
            return new OrderMasterBM(id);
        }

        public int Add(OrderMasterBM entry)
        {
            using (OrderMasterManager m = new OrderMasterManager())
            {
              

                int id= m.Add(entry.OrderMaster);
                using (OrderDetailManager detailManager = new OrderDetailManager())
                {
                    foreach (OrderDetail detail in entry.OrderDetails)
                    {
                        detail.OrderId = id;
                        detailManager.Add(detail);
                    }
                    
                }
              
            }

            return 0;
        }

        public bool Update(OrderMasterBM entry)
        {
            using (OrderMasterManager m = new OrderMasterManager())
            {
                entry.OrderMaster.CustomerId = entry.CustomerBm.CustomerId;
                entry.OrderMaster.PaymentGatewayId = entry.PaymentBm.PaymentId;
                entry.OrderMaster.ShippingId = entry.ShippingBm.ShippingId;

                int id = m.Update(entry.OrderMaster);
                using (OrderDetailManager detailManager = new OrderDetailManager())
                {
                    foreach (OrderDetail detail in entry.OrderDetails)
                    {
                        detail.OrderId = id;
                        detailManager.Update(detail);
                    }

                }

            }

            return false;
        }

        public bool Delete(OrderMasterBM entry)
        {
            using (OrderMasterManager m = new OrderMasterManager())
            {
                try
                {
                    m.Delete(entry.OrderMaster.OrderId);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public void UpdateOrderDetails(List<OrderDetail> sonGelenler, List<OrderDetail> ilkTutulan,int orderMasterId)
        {
            using (OrderDetailManager m = new OrderDetailManager())
            {
                foreach (OrderDetail orderDetail in ilkTutulan)
                {
                    orderDetail.OrderId = orderMasterId;
                    var isDeleted = sonGelenler.FirstOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId) == null;
                    if (isDeleted)
                    {
                        m.Delete(orderDetail.OrderDetailId);

                    }
                }
                foreach (OrderDetail orderDetail in sonGelenler)
                {
                    orderDetail.OrderId = orderMasterId;
                    if (orderDetail.OrderDetailId != 0)
                    {
                        //Update
                        m.Update(orderDetail);
                    }
                    else
                    {
                        m.Add(orderDetail);
                    }
                }


            }
        }
    }
}