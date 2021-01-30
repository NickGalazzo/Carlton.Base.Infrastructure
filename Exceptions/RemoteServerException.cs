using System;
using System.Net.Http;

namespace Carlton.Base.Infrastructure.Exceptions
{
    public class RemoteServerException : CarltonBaseException
    {
        private const string ExceptionMessage = "Error occured while trying to reach remote microservice: {0}";
        public HttpResponseMessage ResponseMessage {get;}

        public RemoteServerException(HttpResponseMessage responseMessage, Exception innerException) : 
            base(string.Format(new System.Globalization.CultureInfo("en-US"), ExceptionMessage, GetRemoteServer(responseMessage)), innerException)
        {
            ResponseMessage = responseMessage;
        }

        private static string GetRemoteServer(HttpResponseMessage message)
        {
            return message.RequestMessage.RequestUri.ToString();
        }
    }
}
