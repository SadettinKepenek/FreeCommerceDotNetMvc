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
    public class UsersBusinessManager : IBusinessOperations<UsersBM>
    {
        public int Add(UsersBM entry)
        {
            using (UsersManager manager = new UsersManager())
            {
                return manager.Add(entry.Users);
            }
            
        }

        public bool Delete(UsersBM entry)
        {
            using (UsersManager manager = new UsersManager())
            {
                return manager.Delete(entry.Users.UserId);
            }
        }

        public void Dispose()
        {
        }

        public List<UsersBM> Get()
        {
            using (UsersManager manager = new UsersManager())
            {
                List<Users> dbUsers = manager.GetAll();
                List<UsersBM> businessModel = new List<UsersBM>();

                foreach (var users in dbUsers)
                {
                   businessModel.Add(new UsersBM(users.UserId));
                }

                return businessModel;
            }
            
        }

        public UsersBM GetById(int id)
        {
            return new UsersBM(id);
        }

        public bool Update(UsersBM entry)
        {
            try
            {
                using (UsersManager manager = new UsersManager())
                {
                    manager.Update(entry.Users);
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