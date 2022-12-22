namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public class ExceptionPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IExceptionHandler _exceptionHandler;

    public ExceptionPipelineBehavior(ILogger logger, IExceptionHandler exceptionHandler) : base(logger)
    {
        _exceptionHandler = exceptionHandler;
    }

    public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogDebug($"Entering Handler of type: {RequestType}");
            var result = await next().ConfigureAwait(false);
            Logger.LogDebug($"Handler handled without exception: {RequestType}");
            return result;
        }
        catch(Exception ex)
        {
            Logger.LogError($"Error occured in handler of type: {RequestType}");
            await _exceptionHandler.HandleException(ex, request).ConfigureAwait(false);
            return await next().ConfigureAwait(false);
        }
    }
}
