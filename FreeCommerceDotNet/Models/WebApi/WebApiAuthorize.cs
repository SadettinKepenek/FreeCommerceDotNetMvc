using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using FreeCommerceDotNet.Models.WebApi.ResponseModels;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace FreeCommerceDotNet.Models.WebApi
{
    public class WebApiAuthorize : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new HttpMessageContent(new HttpResponseMessage(HttpStatusCode.Forbidden))
            };
        }

    }
}