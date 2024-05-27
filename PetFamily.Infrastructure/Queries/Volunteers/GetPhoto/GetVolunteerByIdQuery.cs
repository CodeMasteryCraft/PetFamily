﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public class GetVolunteerByIdQuery
{
    private readonly IMinioProvider _minioProvider;
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteerByIdQuery(
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
            .Include(v => v.Photos)
            .FirstOrDefaultAsync(v => v.Id == request.VolunteerId, cancellationToken: ct);

        if (volunteer is null)
        {
            return Errors.General.NotFound(request.VolunteerId);
        }

        var photoPathes = volunteer.Photos.Select(p => p.Path);

        var photoUrls = await _minioProvider.GetPhotos(photoPathes);
        if (photoUrls.IsFailure)
            return photoUrls.Error;

        var volunteerDto = new VolunteerDto(
            volunteer.Id,
            volunteer.Name,
            volunteer.Photos.Select(p => new VolunteerPhotoDto(p.Id, p.Path, p.IsMain)).ToList());

        return new GetVolunteerByIdResponse(volunteerDto);
    }
}