using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteers.DeletePhoto;

public class DeleteVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeleteVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<bool, Error>> Handle(
        DeleteVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var isRemove =  await _minioProvider.RemovePhoto(request.Path);
        if (isRemove.IsFailure)
            return isRemove.Error;

        // TODO тут нужно удалить фото из БД
        
        return true;
    }
}