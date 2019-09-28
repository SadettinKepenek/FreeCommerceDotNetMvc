using System;
using System.Web.Http;
using FreeCommerceDotNet.Models.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace FreeCommerceDotNet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

        void ConfigureOAuth(IAppBuilder app)
        {
            //Token üretimi için authorization ayarlarını belirliyoruz.
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/getAccessToken"), //Token talebini yapacağımız dizin
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1), //Oluşturulacak tokenı bir gün geçerli olacak şekilde ayarlıyoruz.
                AllowInsecureHttp = true, //Güvensiz http portuna izin veriyoruz.
                Provider = new WebApiProviderAuthorization() //Sağlayıcı sınıfını belirtiyoruz. Birazdan bu sınıfı oluşturacağız.
            };

            //Yukarıda belirlemiş olduğumuz authorization ayarlarında çalışması için server'a ilgili OAuthAuthorizationServerOptions tipindeki nesneyi gönderiyoruz.
            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);

            //Authentication Type olarak Bearer Authentication'ı kullanacağımızı belirtiyoruz.
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //Bearer Token, OAuth 2.0 ile gelen standartlaşmış bir token türüdür.
            //Herhangi bir kriptolu veriye ihtiyaç duyulmaksızın client tarafından token isteğinde bulunulur ve server belirli bir zamana sahip access_token üretir.
            //Bearer Token SSL güvenliğine dayanır.
        }
    }
}