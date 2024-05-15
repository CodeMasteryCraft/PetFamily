using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.DataAccess;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.VolunteerApplications.ApplyVolunteerApplication;

public class ApplyVolunteerApplicationHandler
{
    private readonly IPetFamilyWriteDbContext _dbContext;
    private readonly ILogger<ApplyVolunteerApplicationHandler> _logger;

    public ApplyVolunteerApplicationHandler(
        IPetFamilyWriteDbContext dbContext,
        ILogger<ApplyVolunteerApplicationHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(ApplyVolunteerApplicationRequest request, CancellationToken ct)
    {
        var fullName = FullName.Create(
            request.FirstName, request.LastName, request.Patronymic).Value;

        var application = new VolunteerApplication(
            fullName,
            request.Email,
            request.Description,
            request.YearsExperience,
            request.NumberOfPetsFoundHome,
            request.FromShelter);

        await _dbContext.VolunteersApplications.AddAsync(application, ct);
        await _dbContext.SaveChangesAsync(ct);

        _logger.LogInformation("Volunteer application has been created {id}", application.Id);

        return application.Id;
    }
}