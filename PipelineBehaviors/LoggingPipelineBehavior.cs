namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _stopwatch;

    public LoggingPipelineBehavior(ILogger logger) : base(logger)
    {
        _stopwatch = new Stopwatch();
    }

    public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Handling {RequestType} Request");


        _stopwatch.Start();
        var response = await next().ConfigureAwait(false);
        _stopwatch.Stop();
        Logger.LogDebug($"Method: {next.Method.Name} finished with elapsed time of {_stopwatch.Elapsed.TotalMilliseconds}");
        _stopwatch.Reset();

        Logger.LogInformation($"Finished Handling {RequestType} Request");
        return response;
    }
}
