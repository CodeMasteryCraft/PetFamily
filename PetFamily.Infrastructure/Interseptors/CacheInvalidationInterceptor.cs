using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFamily.Application.Providers;

namespace PetFamily.Infrastructure.Interseptors;

public class CacheInvalidationInterceptor : SaveChangesInterceptor
{
    private readonly ICacheProvider _cacheProvider;

    public CacheInvalidationInterceptor(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        await InvalidateCache(eventData, cancellationToken);

        return result;
    }

    private async Task InvalidateCache(DbContextEventData eventData, CancellationToken ct)
    {
        if (eventData.Context is null)
            return;

        var entries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State
                is EntityState.Added
                or EntityState.Deleted
                or EntityState.Modified);

        foreach (var entry in entries)
        {
            var entityName = entry.Entity.GetType().Name;
            await _cacheProvider.RemoveByPrefixAsync(entityName, ct);
        }
    }
}