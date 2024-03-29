﻿namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public class ResiliancePipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IResiliancePolicyHandler<TResponse> _handler;

    public ResiliancePipelineBehavior(ILogger logger, IResiliancePolicyHandler<TResponse> handler)
        : base(logger)
    {
        _handler = handler;
    }

    public async override Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Logger.LogDebug($"Invoking {RequestType} with resiliant policy");

        var policyResult = await _handler.CreatePolicyWrap()
                                   .ExecuteAndCaptureAsync(async () =>
                                   {
                                       return await next().ConfigureAwait(false);
                                   }).ConfigureAwait(false);

        _handler.HandleResult(policyResult);

        Logger.LogDebug($"Finished invoking method {RequestType} with resiliant policy");
        return policyResult.Result;
    }
}
