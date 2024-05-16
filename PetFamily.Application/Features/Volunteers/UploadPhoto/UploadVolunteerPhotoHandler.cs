using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public class UploadVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public UploadVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<string, Error>> Handle(UploadVolunteerPhotoRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var photoId = Guid.NewGuid();
        var path = photoId + Path.GetExtension(request.File.FileName);

        var photo = VolunteerPhoto.CreateAndActivate(path);
        if (photo.IsFailure)
            return photo.Error;
        
        var isSuccessUpload = volunteer.Value.AddPhoto(photo.Value);
        if (isSuccessUpload.IsFailure)
            return isSuccessUpload.Error;

        var objectName = await _minioProvider.UploadPhoto(request.File, path);
        if (objectName.IsFailure)
            return objectName.Error;

        var result = await _volunteersRepository.Save(ct);
        if (result.IsFailure)
        {
            return result.Error;
        }

        return path;
    }
}