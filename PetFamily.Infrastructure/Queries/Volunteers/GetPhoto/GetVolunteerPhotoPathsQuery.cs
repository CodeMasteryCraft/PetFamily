using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public class GetVolunteerPhotoPathsQuery
{
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteerPhotoPathsQuery(
        PetFamilyReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<GetVolunteerPhotoResponse, Error>> Handle(
        GetVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var volunteer = await _readDbContext.Volunteers
            .Include(v => v.Photos)
            .FirstOrDefaultAsync(v => v.Id == request.VolunteerId, cancellationToken: ct);
    }
}