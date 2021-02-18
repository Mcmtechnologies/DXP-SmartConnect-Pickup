using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class BasePickupResponse
    {
        public HttpResponseMessage Response { get; set; }

        public HttpRequestHeaders RequestHeaders { get; set; }

        public string RequestUrl { get; set; }

        public object RequestData { get; set; }

        public Exception RequestError { get; set; }

        public object Error { get; set; }
    }
}
