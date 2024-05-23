using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public class GetVolunteerPhotoQuery
{
    private readonly IMinioProvider _minioProvider;
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteerPhotoQuery(
        IMinioProvider minioProvider,
        PetFamilyReadDbContext readDbContext)
    {
        _minioProvider = minioProvider;
        _readDbContext = readDbContext;
    }

    public async Task<Result<GetVolunteerByIdResponse, Error>> Handle(
        GetVolunteerPhotoRequest request,
        CancellationToken ct)
    {
        var volunteer = await _readDbContext.Volunteers
            .Include(v => v.Photos).Include(volunteerReadModel => volunteerReadModel.FullName)
            .FirstOrDefaultAsync(v => v.Id == request.VolunteerId, cancellationToken: ct);

        if (volunteer is null)
        {
            return Errors.General.NotFound(request.VolunteerId);
        }

        var photoPathes = volunteer.Photos.Select(p => p.Path);

        var photoUrls = await _minioProvider.GetPhotos(photoPathes, ct);
        if (photoUrls.IsFailure)
            return photoUrls.Error;

        var volunteerDto = new VolunteerDto(
            volunteer.Id,
            volunteer.FullName.FirstName,
            volunteer.Photos.Select(p => new VolunteerPhotoDto
            {
                Id = p.Id,
                Path = p.Path,
                IsMain = p.IsMain
            }).ToList());

        return new GetVolunteerByIdResponse(volunteerDto);
    }
}