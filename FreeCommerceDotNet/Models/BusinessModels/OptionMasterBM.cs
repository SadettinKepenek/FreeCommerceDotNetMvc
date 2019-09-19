using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OptionMasterBM
    {
        public OptionMaster OptionMaster  { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Shipping> Shippings { get; set; }

        public OptionMasterBM(int? id)
        {
            if (id == null)
            {
                OptionMaster = new OptionMaster();
            }
            else
            {
                using (var m = new OptionMasterManager())
                {
                    int key = (int)id;
                    OptionMaster = m.Get(key);
                }
                using (var m = new PaymentsManager())
                {
                    int key = (int)id;
                    Payments = m.GetByIntegerKey(key, "PaymentGateways", "PaymentId");
                }
                using (var m = new ShippingManager())
                {
                    int key = (int)id;
                    Shippings = m.GetByIntegerKey(key, "Shippings", "ShippingId");
                }
            }
        }
    }
}