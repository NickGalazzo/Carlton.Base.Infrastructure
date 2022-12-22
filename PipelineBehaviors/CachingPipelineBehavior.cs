namespace Carlton.Base.Infrastructure.PipelineBehaviors;

public class CachingPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDistributedCache _cache;
    private readonly ICacheKeyGenerator _cacheKeyGenerator;
    private readonly ICacheDurationGenerator _cacheDurationGenerator;

    public CachingPipelineBehavior(ILogger logger, IDistributedCache cache, ICacheKeyGenerator cacheKeyGenerator, ICacheDurationGenerator cacheDurationGenerator) : base(logger)
    {
        _cache = cache;
        _cacheKeyGenerator = cacheKeyGenerator;
        _cacheDurationGenerator = cacheDurationGenerator;
    }

    public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var key = _cacheKeyGenerator.GenerateCacheKey(JsonConvert.SerializeObject(request));
        var cachedValue = await _cache.GetStringAsync(key).ConfigureAwait(false);

        if(cachedValue != null)
        {
            Logger.LogInformation($"{RequestType} Request is retrieving value from Cache");
            Logger.LogDebug($"object being retrieved from cache: {JsonConvert.SerializeObject(RequestType)}");
            return JsonConvert.DeserializeObject<TResponse>(cachedValue);
        }
        else
        {
            Logger.LogInformation($"{RequestType} Request is unable to retrieve value from Memory Cache");
        }

        var response = await next().ConfigureAwait(false);

        if(response != null)
        {
            Logger.LogInformation($"{nameof(response)} is being placed in memory cache");
            Logger.LogDebug($"object being placed in cache: {JsonConvert.SerializeObject(response)}");
            var cacheExpiresIn = _cacheDurationGenerator.GetCacheDuration(response);
            var serializedResponse = JsonConvert.SerializeObject(response);
            await _cache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = cacheExpiresIn }).ConfigureAwait(false);
        }

        return response;
    }
}
