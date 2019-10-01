using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class StoreManager:IStoreService
    {
        private IStoreDal _storeDal;

        public StoreManager(IStoreDal storeDal)
        {
            _storeDal = storeDal;
        }
        public int InsertStore(Store store)
        {
             
            return 0;
        }

        public int UpdateStore(Store store)
        {
            return 0;
        }

        public bool DeleteStore(int id)
        {
            return false;
        }

        public Store SelectStoreById(int id)
        {
            return null;
        }

        public List<Store> SelectAllStores()
        {
            return null;
        }
    }
}