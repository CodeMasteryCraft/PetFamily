using Microsoft.Extensions.Caching.Memory;

namespace PetFamily.Infrastructure.Providers;

public class CacheProvider
{
    private readonly IMemoryCache _memoryCache;

    public CacheProvider(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? TimeSpan.FromMinutes(5));
                return factory(cancellationToken);
            });

        return result;
    }
}