using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OptionMasterBM
    {
        public OptionMaster OptionMaster  { get; set; }
        public List<OptionDetailBM> OptionDetailBms { get; set; }


        public OptionMasterBM(int? id)
        {
            if (id == null)
            {
                OptionMaster = new OptionMaster();
                OptionDetailBms=new List<OptionDetailBM>();
            }
            else
            {
                using (var m = new OptionMasterManager())
                {
                    int key = (int)id;
                    OptionMaster = m.Get(key);
                }

                using (OptionDetailManager m = new OptionDetailManager())
                {
                    var dbDetails = m.GetByIntegerKey((int) id, "OptionsDetail", "OptionId");
                    foreach (var optionDetail in dbDetails)
                    {
                        OptionDetailBms.Add(new OptionDetailBM(optionDetail.ValueId));
                    }
                }              
            }
        }
    }
}