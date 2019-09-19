using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class StoreBM
    {
        public Store Store { get; set; }

        public StoreBM(int? id)
        {
            
            if (id == null)
            {
                Store = new Store();
            }
            else
            {
                
                using (var m = new StoreManager())
                {
                    int key =(int)id;
                    Store = m.Get(key);
                }
            }
        }
    }
}