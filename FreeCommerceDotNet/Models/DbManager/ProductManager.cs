using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.DbManager
{

    public class ProductManager : IOperations<Product>, IDisposable
    {

        public void Dispose()
        {

        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Product entry)
        {
            throw new NotImplementedException();
        }

        public int Update(Product entry)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}