using PetFamily.Application.DataAccess;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets.UploadPhoto;

public class UploadPetPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IPetsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPetPhotoHandler(
        IMinioProvider minioProvider,
        IPetsRepository repository,
        IUnitOfWork unitOfWork)
    {
        _minioProvider = minioProvider;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<string>> Handle(UploadPetPhotoRequest request, CancellationToken ct)
    {
        var pet = await _repository.GetById(request.PetId, ct);
        if (pet.IsFailure)
            return pet.Error;

        var photoId = Guid.NewGuid();
        var path = photoId + Path.GetExtension(request.File.FileName);

        var photo = PetPhoto.CreatePetPhoto(path, request.File.ContentType,
            request.File.Length, request.IsMain);

        if (photo.IsFailure)
            return photo.Error;

        var isSuccessUpload = pet.Value.AddPhoto(photo.Value);
        if (isSuccessUpload.IsFailure)
            return isSuccessUpload.Error;

        var objectName = await _minioProvider.UploadPhoto(request.File, path, ct);
        if (objectName.IsFailure)
            return objectName.Error;

        await _unitOfWork.SaveChangesAsync(ct);

        return path;
    }
}