using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using Microsoft.Owin;

namespace FreeCommerceDotNet.Models.WebApi.Helper
{
    public static class WebAPIHelper
    {
        public static bool isAuthorized(IOwinContext context,int userId)
        {
            var username = context.Authentication.User.Identity.Name;
            using (UserManager bm = new UserManager(new UserRepository()))
            {
                
                var usersBm = bm.SelectById(userId);
                if (context.Authentication.User.IsInRole("Admin"))
                {
                    return true;
                }
                else
                {
                    if (usersBm.Username.Equals(username))
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}