using System.Net;

namespace FreeCommerceDotNet.Models.WebApi.ResponseModels
{
    public class UnauthorizeResponseModel
    {
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}