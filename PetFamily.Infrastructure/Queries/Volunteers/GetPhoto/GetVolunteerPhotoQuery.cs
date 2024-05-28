using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public class GetVolunteerPhotoQuery
{
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteerPhotoQuery(
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
        
        if (volunteer is null)
            return Errors.General.NotFound(request.VolunteerId);
        
        var photo = volunteer.Photos.Select(p => 
            new VolunteerPhotoDto(p.Id, p.Path, p.IsMain)).ToList();

        return new GetVolunteerPhotoResponse(photo);
    }
}