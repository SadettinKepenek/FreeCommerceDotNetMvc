using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ExampleBusinessManager:IBusinessOperations<ExampleBusinessModel>
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public List<ExampleBusinessModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public ExampleBusinessModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ExampleBusinessModel> GetByKey(int id, string tbl, string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Add(ExampleBusinessModel entry)
        {
            using (ProductManager manager = new ProductManager())
            {
            }

            throw new System.NotImplementedException();
        }

        public bool AddRange(List<ExampleBusinessModel> entryList)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(ExampleBusinessModel entry)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteRange(List<ExampleBusinessModel> entryList)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(ExampleBusinessModel entry)
        {
            throw new System.NotImplementedException();
        }
    }
}