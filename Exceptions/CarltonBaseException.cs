using System;
using System.Runtime.Serialization;

namespace Carlton.Base.Infrastructure.Exceptions
{
    public class CarltonBaseException : Exception
    {
        public CarltonBaseException()
        {
        }

        public CarltonBaseException(string message) : base(message)
        {
        }

        public CarltonBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
