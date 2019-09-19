﻿using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class PaymentBM
    {
        public Payment Payment { get; set; }
        public List<OrderMasterBM> OrderMasterBms { get; set; }

        public PaymentBM(int? id)
        {
            if (id == null)
            {
                Payment = new Payment();
            }
            else
            {
                using (var m = new PaymentsManager())
                {
                    int key = (int)id;
                    Payment = m.Get(key);
                    
                }

                using (var m = new OrderMasterManager())
                {
                    var dbOrderMasters =
                        m.GetByIntegerKey((int) id, "OrdersMaster", "PaymentGatewayId");
                    foreach (var dbOrderMaster in dbOrderMasters)
                    {
                        OrderMasterBms.Add(new OrderMasterBM(dbOrderMaster.OrderId));
                    }
                }
            }
        }
    }
}