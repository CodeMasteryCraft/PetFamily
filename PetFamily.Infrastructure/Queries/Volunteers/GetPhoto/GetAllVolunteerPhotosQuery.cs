using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public class GetAllVolunteerPhotosQuery
{
    private readonly IMinioProvider _minioProvider;
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetAllVolunteerPhotosQuery(
        IMinioProvider minioProvider,
        PetFamilyReadDbContext readDbContext)
    {
        _minioProvider = minioProvider;
        _readDbContext = readDbContext;
    }

    public async Task<Result<IReadOnlyList<string>, Error>> Handle(
        GetAllVolunteerPhotoRequest request, 
        CancellationToken ct)
    {
        var volunteerDto = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == request.VolunteerId, cancellationToken: ct);
        if (volunteerDto is null)
            return Errors.General.NotFound(request.VolunteerId);

        var allPhotosDto = volunteerDto.Photos;
        if (allPhotosDto.Count == 0)
            return Errors.General.NotFound();
        
        var paths = allPhotosDto.Select(p => p.Path).ToList();
        if (paths.Count == 0)
            return Errors.General.NotFound();

        return await _minioProvider.GetPhotos(paths);

        
    }
}