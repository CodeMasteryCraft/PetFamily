using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using System.IO;

namespace PetFamily.Application.Features.Volunteers.GetPhotos;

public class GetVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public GetVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<string, ResultEvent>> Handle(GetVolunteerPhotoRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if(volunteer.IsFailure)
            return volunteer.Error;

        var url = await _minioProvider.GetPhoto(request.Photo);
        if (url.IsFailure)
            return url.Error;

        return url;
    }
}
