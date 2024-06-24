using PetFamily.Application.DataAccess;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public class UploadVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ITransaction _transaction;

    public UploadVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository,
        ITransaction transaction)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
        _transaction = transaction;
    }

    public async Task<Result<string>> Handle(UploadVolunteerPhotoRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var photoId = Guid.NewGuid();
        var path = photoId + Path.GetExtension(request.File.FileName);

        var photo = VolunteerPhoto.CreateAndActivate(path, request.File.ContentType,
            request.File.Length, request.IsMain);

        if (photo.IsFailure)
            return photo.Error;

        var isSuccessUpload = volunteer.Value.AddPhoto(photo.Value);
        if (isSuccessUpload.IsFailure)
            return isSuccessUpload.Error;

        await using var stream = request.File.OpenReadStream();
        var objectName = await _minioProvider.UploadPhoto(stream, path, ct);
        if (objectName.IsFailure)
            return objectName.Error;

        await _transaction.SaveChangesAsync(ct);

        return path;
    }
}