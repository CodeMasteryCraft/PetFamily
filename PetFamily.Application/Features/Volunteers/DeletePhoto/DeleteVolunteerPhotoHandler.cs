using CSharpFunctionalExtensions;
using PetFamily.Application.DataAccess;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteers.DeletePhoto;

public class DeleteVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ITransaction _transaction;

    public DeleteVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository,
        ITransaction transaction)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
        _transaction = transaction;
    }

    public async Task<Result<bool, Error>> Handle(
        DeleteVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);   
        if (volunteer.IsFailure)
            return volunteer.Error;

        var isRemove =  await _minioProvider.RemovePhoto(request.Path, ct);
        if (isRemove.IsFailure)
            return isRemove.Error;

        var isDelete = volunteer.Value.DeletePhoto(request.Path);
        if (isDelete.IsFailure)
            return isDelete.Error;

        await _transaction.SaveChangesAsync(ct);

        return true;
    }
}