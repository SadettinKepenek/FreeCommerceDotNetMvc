using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ShippingBM
    {
        public Shipping Shipping { get; set; }
        public List<OrderMaster> OrderMasterBms { get; set; }

        public ShippingBM(int? id)
        {
            if (id == null)
            {
                Shipping = new Shipping();
            }
            else
            {

                using (var m = new ShippingManager())
                {
                    int key = (int)id;
                    Shipping = m.Get(key);
                }
                using (var m = new OrderMasterManager())
                {
                    OrderMasterBms =
                        m.GetByIntegerKey((int)id, "OrdersMaster", "ShippingId");
                   
                }
            }
        }
    }
}