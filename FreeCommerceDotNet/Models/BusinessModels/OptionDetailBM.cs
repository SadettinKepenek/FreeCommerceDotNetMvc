using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OptionDetailBM
    {
        public OptionDetail OptionDetail { get; set; }
        public List<OptionMaster> OptionMasters { get; set; }

        public OptionDetailBM(int? id)
        {
            if (id == null)
            {
                OptionDetail = new OptionDetail();
                OptionMasters = new List<OptionMaster>();
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
                    int key = (int)id;
                    OptionMasters = m.GetByIntegerKey(key, "OrdersMaster", "OrderId");
                }
            }
        }
    }
}