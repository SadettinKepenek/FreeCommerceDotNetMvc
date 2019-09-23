using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class UsersBM
    {
        public Users Users { get; set; }

        public UsersBM(int? id)
        {
            if (id == null)
            {
                Users = new Users();
            }
            else
            {

                using (var m = new UsersManager())
                {
                    int key = (int)id;
                    Users = m.Get(key);
                }
            }
        }
    }
}