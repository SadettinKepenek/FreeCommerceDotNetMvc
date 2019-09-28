using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.WebApi
{
    public class WebApiProviderAuthorization: OAuthAuthorizationServerProvider
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

            string sqlQuery = "select * from Users where Username=@username and Password=@password";
            SqlCommand cmd = new SqlCommand(sqlQuery);
            cmd.Parameters.AddWithValue("@username", context.UserName);
            cmd.Parameters.AddWithValue("@password", context.Password);
            using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
            {
                cmd.Connection = connection;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        var roles = reader[4].ToString();
                        ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim("sub", context.UserName));
                        foreach (var role in roles.Split(';'))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));

                        }

                        context.Validated(identity);
                    }

                }
                else
                {
                    context.SetError("invalid_grant", "Kullanıcı adı veya şifre hatalı.");
                }
                connection.Close();
            }



        }
    }
}