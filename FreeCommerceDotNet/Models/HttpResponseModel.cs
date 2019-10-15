using System.Net;

namespace FreeCommerceDotNet.Models.Abstracts
{
    public abstract class HttpResponseModel<T>
    {
        public virtual HttpStatusCode StatusCode { get; set; }
        public virtual string ResponseText { get; set; }
        public virtual T ResponseObject { get; set; }

    }
}