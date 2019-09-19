using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OptionDetailBM
    {
        public OptionDetail OptionDetail { get; set; }
        public OptionMaster OptionMaster { get; set; }

        public OptionDetailBM(int? id)
        {
            if (id == null)
            {
                OptionDetail = new OptionDetail();
                OptionMaster = new OptionMaster();
            }
            else
            {

                using (var m = new OptionDetailManager())
                {
                    int key = (int)id;
                    OptionDetail = m.Get(key);

                }

                using (var m = new OptionMasterManager())
                {
                    OptionMaster = m.Get(OptionDetail.OptionId);

                }

            }
        }
    }
}