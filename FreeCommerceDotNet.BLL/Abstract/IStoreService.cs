using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Abstract
{
    public interface IStoreService
    {
        int InsertStore(Store store);
        int UpdateStore(Store store);
        bool DeleteStore(int id);
        Store SelectStoreById(int id);
        List<Store> SelectAllStores();

    }
}