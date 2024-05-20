using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.BackgroundServices;

public class ImageCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _provider;
    
    public ImageCleanupService(IServiceScopeFactory provider)
    {
        _provider = provider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        var minioClient = scope.ServiceProvider.GetRequiredService<IMinioClient>();
        var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyReadDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ImageCleanupService>>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(10000, stoppingToken);
            logger.LogInformation("Cleaning up unused images...");

            try
            {
                var listObjectArgs = new ListObjectsArgs().WithBucket("images");

                var objectList = minioClient.ListObjectsAsync(listObjectArgs, stoppingToken);
                var volunteerReadModels = dbContext.Volunteers
                    .Include(p => p.Photos);
                var paths = volunteerReadModels.Select(p => p.Photos.Select(ph => ph.Path));
                foreach (var obj in objectList)
                {
                    if (!paths.Any(p => p.Contains(obj.Key)))
                    {
                        try
                        {
                            var removeObjectArgs = new RemoveObjectArgs().WithBucket("images").WithObject(obj.Key);

                            await minioClient.RemoveObjectAsync(removeObjectArgs, stoppingToken);
                            logger.LogInformation($"Image {obj.Key} has been deleted from MinIO storage.");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Error deleting image {obj.Key} from MinIO storage.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error cleaning up unused images.");
            }
        }
    }
}