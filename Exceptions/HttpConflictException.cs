namespace Carlton.Base.Infrastructure.Exceptions
{
    public class HttpConflictException : CarltonBaseException
    {
        public HttpConflictException() : base("Unable to complete request due to a conflict with the current state of the target resource.")
        {

        }
    }
}
