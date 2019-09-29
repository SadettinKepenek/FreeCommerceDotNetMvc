using System.Web.Routing;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.DbModels;
using Microsoft.Owin;

namespace FreeCommerceDotNet.Models.WebApi.Helper
{
    public static class WebAPIHelper
    {
        public static bool isAuthorized(IOwinContext context,int userId)
        {
            var username = context.Authentication.User.Identity.Name;
            using (UsersBusinessManager bm = new UsersBusinessManager())
            {
                var usersBm = bm.GetById(userId);
                if (context.Authentication.User.IsInRole("Admin"))
                {
                    return true;
                }
                else
                {
                    if (usersBm.Users.Username.Equals(username))
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}