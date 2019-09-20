using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ShippingBusinessManager : IBusinessOperations<ShippingBM>
    {
        public int Add(ShippingBM entry)
        {
            using (ShippingManager manager = new ShippingManager())
            {
                return manager.Add(entry.Shipping);
            }
        }

        public bool Delete(ShippingBM entry)
        {
            using (ShippingManager manager = new ShippingManager())
            {
                return manager.Delete(entry.Shipping.ShippingId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<ShippingBM> Get()
        {
            using (ShippingManager manager = new ShippingManager())
            {
                List<Shipping> dbStores = manager.GetAll();
                List<ShippingBM> businessModels = new List<ShippingBM>();

                foreach (var shipping in dbStores)
                {
                    businessModels.Add(new ShippingBM(shipping.ShippingId));
                }

                return businessModels;
            }
        }

        public ShippingBM GetById(int id)
        {
            return new ShippingBM(id);
        }

        public bool Update(ShippingBM entry)
        {
            try
            {
                using (ShippingManager manager = new ShippingManager())
                {
                    manager.Update(entry.Shipping);
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