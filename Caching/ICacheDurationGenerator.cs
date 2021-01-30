using System;

namespace Carlton.Base.Infrastructure.Caching
{
    public interface ICacheDurationGenerator
    {
        TimeSpan GetCacheDuration(object obj);
    }
}
