using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace Carlton.Base.Infrastructure.Extensions
{
    public static class DistributedCaching
    {
        public async static Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            await distributedCache.SetAsync(key, value.ToByteArray(), options, token).ConfigureAwait(false);
        }

        public async static Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default) where T : class
        {
            var result = await distributedCache.GetAsync(key, token).ConfigureAwait(false);
            return result.FromByteArray<T>();
        }
    }
}
