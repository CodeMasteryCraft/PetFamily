using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Application.Providers;

namespace PetFamily.Infrastructure.Jobs;

public class ImageCleanupJob : IImageCleanupJob
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<ImageCleanupJob> _logger;

    public ImageCleanupJob(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository,
        ILogger<ImageCleanupJob> logger)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task ProccessAsync()
    {
        var cancellationToken = new CancellationTokenSource().Token;

        _logger.LogInformation("Cleaning up unused images...");

        List<string> storagePhotoPaths = [];

        var objectList = _minioProvider.GetObjectsList(cancellationToken);

        objectList.Subscribe(item => storagePhotoPaths.Add(item.Key));
        await objectList.LastOrDefaultAsync();

        if (storagePhotoPaths.Count == 0)
        {
            _logger.LogInformation("No images to delete");
            return;
        }

        var volunteers = await _volunteersRepository.GetAllWithPhotos(cancellationToken);

        var photoPaths = volunteers
            .SelectMany(v => v.Photos)
            .Select(p => p.Path)
            .ToList();

        var extraPaths = storagePhotoPaths
            .Except(photoPaths)
            .ToList();

        if (extraPaths.Count == 0)
        {
            _logger.LogInformation("No images to delete");

            return;
        }

        var removeResult = await _minioProvider.RemovePhotos(extraPaths, cancellationToken);

        if (removeResult.IsFailure)
        {
            _logger.LogError("Error deleting images from MinIO storage.");
        }

        _logger.LogInformation("Images has been deleted from MinIO storage.");
    }
}