using Microsoft.Extensions.Logging;
using PetFamily.Application.DataAccess;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.VolunteerApplications.ApplyVolunteerApplication;

public class ApplyVolunteerApplicationHandler
{
    private readonly ITransaction _transaction;
    private readonly IVolunteerApplicationsRepository _volunteerApplicationsRepository;
    private readonly ILogger<ApplyVolunteerApplicationHandler> _logger;

    public ApplyVolunteerApplicationHandler(
        ITransaction transaction,
        IVolunteerApplicationsRepository volunteerApplicationsRepository,
        ILogger<ApplyVolunteerApplicationHandler> logger)
    {
        _transaction = transaction;
        _volunteerApplicationsRepository = volunteerApplicationsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(ApplyVolunteerApplicationRequest request, CancellationToken ct)
    {
        var fullName = FullName.Create(
            request.FirstName, request.LastName, request.Patronymic).Value;

        var email = Email.Create(request.Email).Value;

        var application = new VolunteerApplication(
            fullName,
            email,
            request.Description,
            request.YearsExperience,
            request.NumberOfPetsFoundHome,
            request.FromShelter);

        await _volunteerApplicationsRepository.Add(application, ct);
        await _transaction.SaveChangesAsync(ct);

        _logger.LogInformation("Volunteer application has been created {id}", application.Id);

        return application.Id;
    }
}