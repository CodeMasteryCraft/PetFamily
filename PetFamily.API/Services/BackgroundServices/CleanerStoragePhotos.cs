using Hangfire;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.API.Services.BackgroundServices;

public class CleanerStoragePhotos : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly PetFamilyReadDbContext _context;
    private readonly IMinioProvider _minioProvider;

    public CleanerStoragePhotos(
        IServiceScopeFactory serviceScopeFactory, 
        PetFamilyReadDbContext context,
        IMinioProvider minioProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _context = context;
        _minioProvider = minioProvider;
    }


    private async Task DeletePhotos()
    {
        var photoDb = _context.Volunteers
            .Include(v => v.Photos)
            .SelectMany(r => r.Photos)
            .Select(p => p.Path).Where(s => s.Trim().Length > 0).ToList();

        var photoPaths = _minioProvider.PhotoPaths("images").Result.Value;
        if (photoPaths is null)
            return;

        foreach (var path  in photoPaths)
        {
            if (!photoDb.Any(p => p.Contains(path)))
                await _minioProvider.RemovePhoto(path);

        }
    }
  
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyReadDbContext>();
         RecurringJob.AddOrUpdate(
            "cleaner",
            () => DeletePhotos(),
            Cron.Daily);
    }
}

