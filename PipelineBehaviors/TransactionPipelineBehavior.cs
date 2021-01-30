using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Carlton.Base.Infrastructure.PipelineBehaviors
{
    public class TransactionPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    {
        public TransactionPipelineBehavior(ILogger logger) : base(logger)
        {
        }

        public async override Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                using(var transaction = new TransactionScope())
                {
                    Logger.LogInformation($"Begining Transaction for {RequestType}");

                    var result = await next().ConfigureAwait(false);
                    transaction.Complete();
                    Logger.LogInformation($"End Transaction for {RequestType}");
                    return result;
                }
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
