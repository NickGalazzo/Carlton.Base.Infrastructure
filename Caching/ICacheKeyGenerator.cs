namespace Carlton.Base.Infrastructure.Caching
{
    public interface ICacheKeyGenerator
    {
        string GenerateCacheKey(object obj);
    }
}
