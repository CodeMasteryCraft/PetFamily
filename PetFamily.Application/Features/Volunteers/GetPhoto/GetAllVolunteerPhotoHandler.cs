using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteers.GetPhoto;

public class GetAllVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public GetAllVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersRepository volunteersRepository)
    {
        _minioProvider = minioProvider;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<IReadOnlyList<string>, Error>> Handle(GetAllVolunteerPhotoRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var allPhotos = volunteer.Value.Photos;
        if (allPhotos.Count == 0)
            return Errors.General.NotFound();
        
        var paths = allPhotos.Select(p => p.Path).ToList();
        if (paths.Count == 0)
            return Errors.General.NotFound();

        return await _minioProvider.GetPhotos(paths);

        
    }
}