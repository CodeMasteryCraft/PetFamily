using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetVolunteer;

public class GetVolunteerQuery
{
    private IMinioProvider _minioProvider;
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteerQuery(PetFamilyReadDbContext readDbContext, IMinioProvider minioProvider)
    {
        _minioProvider = minioProvider;
        _readDbContext = readDbContext;
    }
    public async Task<Result<GetVolunteerResponse, Error>> Handle(
        GetVolunteerRequest request,
        CancellationToken ct)
    {
        var volunteer = await _readDbContext.Volunteers
            .Include(v => v.Photos)
            .Include(p => p.Pets)
            .FirstOrDefaultAsync(v => v.Id == request.VolunteerId, cancellationToken: ct);

        if (volunteer is null)
        {
            return Errors.General.NotFound(request.VolunteerId);
        }

        var photoPaths = volunteer.Photos.Select(p => p.Path);

        var photoUrls = await _minioProvider.GetPhotos(photoPaths, ct);
        if (photoUrls.IsFailure)
            return photoUrls.Error;

        var volunteerDto = new VolunteerDto(
            volunteer.Id,
            volunteer.FirstName,
            volunteer.LastName,
            volunteer.Patronymic,
            volunteer.Photos.Select(p => new VolunteerPhotoDto
            {
                Id = p.Id,
                Path = p.Path,
                IsMain = p.IsMain
            }).ToList());

        return new GetVolunteerResponse(volunteerDto);
    }
}