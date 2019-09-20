using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class StoreBusinessManager : IBusinessOperations<StoreBM>
    {
        public int Add(StoreBM entry)
        {
            using (StoreManager manager = new StoreManager())
            {
                return manager.Add(entry.Store);
            }
        }

        public bool Delete(StoreBM entry)
        {
            using (StoreManager manager = new StoreManager())
            {
                return manager.Delete(entry.Store.StoreId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<StoreBM> Get()
        {
            using (StoreManager manager = new StoreManager())
            {
                List<Store> dbStores = manager.GetAll();
                List<StoreBM> businessModels = new List<StoreBM>();

                foreach (var stores in dbStores)
                {
                    businessModels.Add(new StoreBM(stores.StoreId));
                }

                return businessModels;
            }

        }

        public StoreBM GetById(int id)
        {
            
                return new StoreBM(id);
            
        }

        public bool Update(StoreBM entry)
        {
            try
            {
                using (StoreManager manager = new StoreManager())
                {
                    manager.Update(entry.Store);
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