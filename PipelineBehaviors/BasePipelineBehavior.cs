namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public abstract class BasePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected ILogger Logger { get; }

    protected string RequestType
    {
        get { return $"{typeof(TRequest).Name} Request"; }
    }

    public BasePipelineBehavior(ILogger logger)
    {
        Logger = logger;
    }

    public abstract Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}
