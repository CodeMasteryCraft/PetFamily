using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.DataAccess;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.VolunteerApplications.ApplyVolunteerApplication;

public class ApplyVolunteerApplicationHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerApplicationsRepository _volunteerApplicationsRepository;
    private readonly ILogger<ApplyVolunteerApplicationHandler> _logger;

    public ApplyVolunteerApplicationHandler(
        IUnitOfWork unitOfWork,
        IVolunteerApplicationsRepository volunteerApplicationsRepository,
        ILogger<ApplyVolunteerApplicationHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _volunteerApplicationsRepository = volunteerApplicationsRepository;
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

        await _volunteerApplicationsRepository.Add(application, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation("Volunteer application has been created {id}", application.Id);

        return application.Id;
    }
}