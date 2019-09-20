using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class OptionMasterBusinessManager : IBusinessOperations<OptionMasterBM>
    {
        public int Add(OptionMasterBM entry)
        {
            using (OptionMasterManager manager = new OptionMasterManager())
            {
                int insertedId = manager.Add(entry.OptionMaster);
                entry.OptionMaster.OptionId = insertedId;
                using (OptionDetailManager manager2 = new OptionDetailManager())
                {
                    foreach (var details in entry.OptionDetailBms)
                    {
                        details.OptionId = entry.OptionMaster.OptionId;
                        manager2.Add(details);

                    }

                    return entry.OptionMaster.OptionId;
                }
            }
        }

        public bool Delete(OptionMasterBM entry)
        {
            using (OptionMasterManager manager = new OptionMasterManager())
            {
                return manager.Delete(entry.OptionMaster.OptionId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<OptionMasterBM> Get()
        {
            using (OptionMasterManager manager = new OptionMasterManager())
            {
                List<OptionMaster> dbOptionMasters = new List<OptionMaster>();
                List<OptionMasterBM> businessModel = new List<OptionMasterBM>();

                foreach (var optionMaster in dbOptionMasters)
                {
                    businessModel.Add(new OptionMasterBM(optionMaster.OptionId));
                }

                return businessModel;
            }
        }

        public OptionMasterBM GetById(int id)
        {
           return new OptionMasterBM(id);
        }

        public bool Update(OptionMasterBM entry)
        {
            try
            {
                
                using (OptionMasterManager manager = new OptionMasterManager())
                {
                    int updatedId = manager.Update(entry.OptionMaster);
                    entry.OptionMaster.OptionId = updatedId;
                    using (OptionDetailManager manager2 = new OptionDetailManager())
                    {
                        foreach (var details in entry.OptionDetailBms)
                        {
                            details.OptionId = entry.OptionMaster.OptionId;
                            manager2.Update(details);
                        }
                    }
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