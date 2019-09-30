using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using Microsoft.Ajax.Utilities;

namespace FreeCommerceDotNet.Models.BusinessManager 
{
    public class BrandBusinessManager : IBusinessOperations<BrandBM>
    {
        public int Add(BrandBM entry)
        {
            using (BrandManager manager = new BrandManager())
            {
                return manager.Add(entry.Brand);
            }
        }

        public bool Delete(BrandBM entry)
        {
            using (BrandManager manager = new BrandManager())
            {
                return manager.Delete(entry.Brand.BrandId);
            }
        }

        public void Dispose()
        {
        }

        public List<BrandBM> Get()
        {
            using (BrandManager manager = new BrandManager())
            {
                List<Brand> dbBrands = manager.GetAll();
                List<BrandBM> businessModel = new List<BrandBM>();

                foreach (var brands in dbBrands)
                {
                    businessModel.Add(new BrandBM(brands.BrandId));
                }

                return businessModel;
            }
        }

        public BrandBM GetById(int id)
        {
            return new BrandBM(id);
        }

        public bool Update(BrandBM entry)
        {
            try
            {
                using (BrandManager manager = new BrandManager())
                {
                    manager.Update(entry.Brand);
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