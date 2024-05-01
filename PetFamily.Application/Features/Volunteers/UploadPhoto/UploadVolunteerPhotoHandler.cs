using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public class UploadVolunteerPhotoHandler
{
    private readonly IMinioService _minioService;
    private readonly IVolunteersRepository _volunteersRepository;

    public UploadVolunteerPhotoHandler(
        IMinioService minioService,
        IVolunteersRepository volunteersRepository)
    {
        _minioService = minioService;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<string, Error>> Handle(UploadVolunteerPhotoRequest request)
    {
        await using (var stream = request.File.OpenReadStream())
        {
            var path = Guid.NewGuid().ToString();

            var success = await _minioService.UploadPhoto(stream, path);

            if (success.IsFailure)
                return success.Error;
        }
    }
}