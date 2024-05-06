using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
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

    public async Task<Result<string, Error>> Handle(DeleteVolunteerPhotoRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var dbRemove = await _volunteersRepository.DeletePhoto(request.Path, ct);
        if (dbRemove.IsFailure)
            return dbRemove.Error;

        var minioRemove = await _minioProvider.RemovePhoto(request.Path, ct);
        if (minioRemove.IsFailure)
            return minioRemove.Error;

        return "remove seccess";
    }
}
