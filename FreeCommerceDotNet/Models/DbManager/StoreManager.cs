using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class StoreManager : IOperations<Store>, IDisposable
    {
        public bool Add(Store entry)
        {
            
            using(SqlCommand command = new SqlCommand("sp_store_insert"))
            {
                var sqlCommand = command;
                var stores = new List<Store>();
                Utilities.ExecuteCommand<Store>(sqlCommand, SqlCommandTypes.Select, ref stores);
                return false;

            }
            throw new NotImplementedException();
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Store Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(Store entry)
        {
            throw new NotImplementedException();
        }
    }
}