using Polly;
using System.Net.Http;

namespace DXP.SmartConnectPickup.Common.WebApi
{
    public interface IWebApiPolicyFactory
    {
        IAsyncPolicy<HttpResponseMessage> CreateMainPolicy(string circuitName);
        IAsyncPolicy<HttpResponseMessage> CreateNoOpPolicy();
        IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy();
        IAsyncPolicy<HttpResponseMessage> CreateCircuitBreakerPolicy(string name);
        IAsyncPolicy<HttpResponseMessage> CreateTimeoutPolicy();
    }
}
