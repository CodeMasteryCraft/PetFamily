using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public class GetAllVolunteerPhotoHandler
{
    private readonly IMinioProvider _minioProvider;
    private readonly IVolunteersQuery _volunteersQuery;

    public GetAllVolunteerPhotoHandler(
        IMinioProvider minioProvider,
        IVolunteersQuery volunteersQuery)
    {
        _minioProvider = minioProvider;
        _volunteersQuery = volunteersQuery;
    }

    public async Task<Result<IReadOnlyList<string>, Error>> Handle(
        GetAllVolunteerPhotoRequest request, 
        CancellationToken ct)
    {
        var volunteerDto = await _volunteersQuery.GetById(request.VolunteerId, ct);
        if (volunteerDto.IsFailure)
            return volunteerDto.Error;

        var allPhotosDto = volunteerDto.Value.Photos;
        if (allPhotosDto.Count == 0)
            return Errors.General.NotFound();
        
        var paths = allPhotosDto.Select(p => p.Path).ToList();
        if (paths.Count == 0)
            return Errors.General.NotFound();

        return await _minioProvider.GetPhotos(paths);

        
    }
}