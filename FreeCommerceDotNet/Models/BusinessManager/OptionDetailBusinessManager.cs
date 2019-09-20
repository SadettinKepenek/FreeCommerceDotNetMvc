using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class OptionDetailBusinessManager : IBusinessOperations<OptionDetailBM>
    {
        public int Add(OptionDetailBM entry)
        {
            using (OptionDetailManager manager = new OptionDetailManager())
            {
                entry.OptionDetail.OptionId = entry.OptionMaster.OptionId;
                return manager.Add(entry.OptionDetail);

            }
        }

        public bool Delete(OptionDetailBM entry)
        {
            using (OptionDetailManager manager = new OptionDetailManager())
            {
                return manager.Delete(entry.OptionMaster.OptionId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<OptionDetailBM> Get()
        {
            using (OptionDetailManager manager = new OptionDetailManager())
            {
                List<OptionDetail> dbOptionDetail = manager.GetAll();
                List<OptionDetailBM> businessModels = new List<OptionDetailBM>();

                foreach (var details in dbOptionDetail)
                {
                    businessModels.Add(new OptionDetailBM(details.OptionId));
                }

                return businessModels;
            }
        }

        public OptionDetailBM GetById(int id)
        {
            return new OptionDetailBM(id);
        }

        public bool Update(OptionDetailBM entry)
        {
            try
            {
                using (OptionDetailManager manager = new OptionDetailManager())
                {
                    manager.Update(entry.OptionDetail);
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