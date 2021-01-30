using Carlton.Base.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Carlton.Base.Infrastructure.Resiliance
{
    public class HttpResiliancePolicyHandler : IResiliancePolicyHandler<HttpResponseMessage>
    {
        private readonly ILogger<HttpResiliancePolicyHandler> _logger;

        public HttpResiliancePolicyHandler(ILogger<HttpResiliancePolicyHandler> logger)
        {
            _logger = logger;
        }

        public AsyncPolicyWrap<HttpResponseMessage> CreatePolicyWrap()
        {
            HttpStatusCode[] httpStatusCodesWorthRetrying = {
               HttpStatusCode.RequestTimeout, // 408
               HttpStatusCode.InternalServerError, // 500
               HttpStatusCode.BadGateway, // 502
               HttpStatusCode.ServiceUnavailable, // 503
               HttpStatusCode.GatewayTimeout // 504
            };

            var policy = Policy
               .Handle<HttpRequestException>()
               .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
               .RetryAsync(3, (exception, retryCount, context) =>
               {
                   var methodThatRaisedException = context["methodName"];
                   _logger.LogWarning(exception.Exception, $"Exception occured in method {methodThatRaisedException}, retrying HTTP call. Retry Count {retryCount}");
               });

            var policyWrap = Policy.WrapAsync(policy);

            return policyWrap;
        }

        public void HandleResult(PolicyResult<HttpResponseMessage> policyResult)
        {
            if (policyResult.Outcome == OutcomeType.Failure)
            {
                var requestUrl = policyResult.Result.RequestMessage.RequestUri;
                _logger.LogWarning($"Failed to reach remote server: {requestUrl} with resiliance policy, throwing exception");
                throw new RemoteServerException(policyResult.Result, policyResult.FinalException);
            }
        }
    }
}
