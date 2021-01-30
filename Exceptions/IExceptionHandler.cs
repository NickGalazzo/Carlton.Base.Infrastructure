using System;
using System.Threading.Tasks;

namespace Carlton.Base.Infrastructure.Exceptions
{
    public interface IExceptionHandler
    {
        Task HandleException(Exception ex, object requestObject);
    }
}
