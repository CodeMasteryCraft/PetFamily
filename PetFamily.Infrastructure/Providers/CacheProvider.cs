using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using PetFamily.Application.Providers;

namespace PetFamily.Infrastructure.Providers;

public class CacheProvider : ICacheProvider
{
    private static readonly ConcurrentDictionary<string, bool> cacheKeys = new();
    private readonly IDistributedCache _cache;

    public CacheProvider(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
        where T : class
    {
        var cachedValue = await _cache.GetStringAsync(key, token: ct);

        if (cachedValue is null)
            return null;

        var value = JsonSerializer.Deserialize<T>(cachedValue);

        return value;
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken ct = default)
        where T : class
    {
        var cachedValue = await GetAsync<T>(key, ct);

        if (cachedValue is not null)
            return cachedValue;

        cachedValue = await factory();

        await SetAsync(key, cachedValue, ct);

        return cachedValue;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken ct = default)
    {
        var stringValue = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, stringValue, token: ct);
        cacheKeys.TryAdd(key, true);
    }

    public async Task RemoveAsync(string key, CancellationToken ct = default)
    {
        await _cache.RemoveAsync(key, ct);
        cacheKeys.TryRemove(key, out _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken ct = default)
    {
        var tasks = cacheKeys.Keys
            .Where(k => k.StartsWith(prefixKey.ToLower()))
            .Select(k => RemoveAsync(k, ct));

        await Task.WhenAll(tasks);
    }
}