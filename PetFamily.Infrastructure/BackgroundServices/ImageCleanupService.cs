using System.Reactive.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.BackgroundServices;

public class ImageCleanupService : BackgroundService
{
    private readonly IServiceProvider _provider;
    
    public ImageCleanupService(IServiceProvider provider)
    {
        _provider = provider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        var minioClient = scope.ServiceProvider.GetRequiredService<IMinioClient>();
        var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyWriteDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ImageCleanupService>>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(10000, stoppingToken);
            logger.LogInformation("Cleaning up unused images...");

            try
            {
                var listObjectArgs = new ListObjectsArgs().WithBucket("images");

                var objectList = minioClient.ListObjectsAsync(listObjectArgs, stoppingToken);
                var paths = dbContext.Photos.Select(p => p.Path);

                foreach (var obj in objectList)
                {
                    if (!paths.Contains(obj.Key))
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