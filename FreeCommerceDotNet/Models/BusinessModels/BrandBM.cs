using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class BrandBM
    {
        public Brand Brand { get; set; }

        public BrandBM()
        {
            Brand = new Brand();
        }
        public BrandBM(int? id)
        {
            if (id == null)
            {
                Brand = new Brand();
            }
            else
            {

                using (var m = new BrandManager())
                {
                    int key = (int)id;
                    Brand = m.Get(key);
                }
            }
        }
    }
}