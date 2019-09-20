using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class PaymentBusinessManager : IBusinessOperations<PaymentBM>
    {
        public int Add(PaymentBM entry)
        {
            using (PaymentsManager manager = new PaymentsManager())
            {
                return manager.Add(entry.Payment);
            }
        }

        public bool Delete(PaymentBM entry)
        {
            using (PaymentsManager manager = new PaymentsManager())
            {
                return manager.Delete(entry.Payment.PaymentId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<PaymentBM> Get()
        {
            using (PaymentsManager manager = new PaymentsManager())
            {
                List<Payment> dbPayment = manager.GetAll();
                List<PaymentBM> bussinessModels = new List<PaymentBM>();

                foreach (var payment in dbPayment)
                {
                    bussinessModels.Add(new PaymentBM(payment.PaymentId));
                }

                return bussinessModels;
            }
        }

        public PaymentBM GetById(int id)
        {
            return new PaymentBM(id);
        }

        public bool Update(PaymentBM entry)
        {
            try
            {
                using (PaymentsManager manager = new PaymentsManager())
                {
                    manager.Update(entry.Payment);
                }
                return true;
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
    }
}