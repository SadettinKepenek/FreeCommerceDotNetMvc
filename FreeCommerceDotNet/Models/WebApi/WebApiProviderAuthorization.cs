using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.Models.WebApi
{
    public class WebApiProviderAuthorization : OAuthAuthorizationServerProvider
    {
        /// Kullanımı
        /// PostMan ile siteurl.com/token adresine post isteği yapılır
        /// Headers'a  : Accept:application/json ve Content-Type:application/x-www-form-urlencoded parametreleri eklenir
        /// Body'e :grant_type : password , username : username , password:password olarak eklenir doğru giriş yapılırsa
        ///     "access_token": "fOsKoAw0NK2t2xid3BUVTtJiy6q7e86_8WUwkyErBsI8PQYPvXfyWbdoG9bPQICtx6BcfF3dpfeMh2UUIWPtmaT9dXRec7zwfsDBj9AExF5-JGINqab5tg8BZgbLzWLiCPgfCzu0SgxTLdnoM8FhsFOQnbeTxx_8dOVtR-v6tfubIoc08lLJ0qLcPpNKVyykdDvoYtd0AWRXUI5IsCsvPmrO_SCmjFYSVhZndbphu80zT8tKlMhpvBC2XddLIH57b7hfx7zutkGjZc_Dirkp8XvFlFnejrqGAIBGA51T1CLrW2x5PyuEufNJcU_AalIZ",
        ///  "token_type": "bearer",
        /// expires_in": 86399 şeklinde bir response gelir buradaki token mobile uygulama için ve get isteklerinde kullanılır
        /// yine headersa token alınırken eklenen headerlar eklenir sonrasında Authorization:Bearer token
        /// token yazan yere access_tokendan gelen token yazılır ve bu token ile işlem sağlanır.

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Domainler arası etkileşim ve kaynak paylaşımını sağlayan ve bir domainin bir başka domainin kaynağını kullanmasını sağlayan CORS ayarlarını set ediyoruz.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Kullanıcının access_token alabilmesi için gerekli validation işlemlerini yapıyoruz.
            using (UserManager m = new UserManager(new UserRepository()))
            {
                var user = m.SelectByFilter(new List<DBFilter>()
                {
                    new DBFilter()
                {
                        ParamName = "@username",ParamValue = context.UserName
                },
                    new  DBFilter()
                    {
                        ParamName = "@password",ParamValue = context.Password
                    }

                }).FirstOrDefault();
                if (user != null)
                {
                    var roles = user.Role;
                    ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim(System.IdentityModel.Claims.ClaimTypes.Name, context.UserName));
                    foreach (var role in roles.Split(';'))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));

                    }

                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Kullanıcı adı veya şifre hatalı.");

                }

            }



        }
    }
}