using System.Collections.Generic;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class StoreRepository:IStoreDal
    {
        public int Insert(Store entity)
        {
            return 0;
        }

        public int Update(Store entity)
        {
            return 0;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public Store SelectById(int id)
        {
            return null;
        }

        public List<Store> SelectAll()
        {
            return null;
        }
    }
}