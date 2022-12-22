namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public class TransactionPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public TransactionPipelineBehavior(ILogger logger) : base(logger)
    {
    }

    public async override Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
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
