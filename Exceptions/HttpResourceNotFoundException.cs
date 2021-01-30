namespace Carlton.Base.Infrastructure.Exceptions
{
    public class HttpResourceNotFoundException : CarltonBaseException
    {
        public HttpResourceNotFoundException() : base("The server was unable to locate the requested resource")
        {
        }
    }
}
