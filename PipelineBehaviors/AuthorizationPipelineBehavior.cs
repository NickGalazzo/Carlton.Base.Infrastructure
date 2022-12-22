namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public class AuthorizationPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IAuthorizer<TRequest> _authorizer;

    public AuthorizationPipelineBehavior(ILogger logger, IAuthorizer<TRequest> authorizer) : base(logger)
    {
        _authorizer = authorizer;
    }

    public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Attempting to authorize user accessing: {RequestType}");

        if(!_authorizer.IsAuthorized(request))
        {
            Logger.LogInformation($"Unauthorized access attempt on {RequestType}");
            throw new UnauthorizedAccessException("User attempting to access resource they are not authorized to");
        }

        Logger.LogInformation($"Access to {RequestType} granted");
        return await next().ConfigureAwait(false);
    }
}


