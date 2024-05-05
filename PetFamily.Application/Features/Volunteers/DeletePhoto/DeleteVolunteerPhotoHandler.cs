using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using System.IO;

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

    public async Task<Result<ResultEvent>> Handle(DeleteVolunteerPhotoRequest request, CancellationToken ct)
    {
        var valanteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (valanteer.IsFailure)
            return valanteer.Error;

        var result = await _minioProvider.RemovePhoto(request.Path, ct);
        if (result.IsFailure)
            Errors.General.DeleteFailure();

        return Seccess.Ok();
    }
}
