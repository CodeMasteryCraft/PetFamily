using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Features.Volunteers.GetPhotos;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers.GetsPhotos;

public class GetVolunteerAllPhotosHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public GetVolunteerAllPhotosHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<IReadOnlyList<string>, Error>> Handle(GetVolunteerAllPhotosRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var photoPath = await _volunteersRepository.GetByPhotos(request.VolunteerId, ct);
        if (photoPath.IsFailure)
            return photoPath.Error;

        var urls = await _minioProvider.GetPhotos(photoPath.Value, ct);
        if (urls.IsFailure)
            return urls.Error;

        return urls;
    } 
}
