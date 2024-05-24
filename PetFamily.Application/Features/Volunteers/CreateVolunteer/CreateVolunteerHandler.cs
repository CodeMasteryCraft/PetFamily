using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.DataAccess;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken ct)
    {
        var socialMedias = request.SocialMedias?
            .Select(s =>
            {
                var social = Social.Create(s.Social).Value;
                return SocialMedia.Create(s.Link, social).Value;
            }) ?? [];

        var fullName = FullName.Create(
            request.FirstName, request.LastName, request.Patronymic).Value;

        var volunteer = new Volunteer(
            fullName,
            request.Description,
            request.YearsExperience,
            request.NumberOfPetsFoundHome,
            request.DonationInfo,
            request.FromShelter,
            socialMedias);

        await _volunteersRepository.Add(volunteer, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation("Volunteer with {id} has been created", volunteer.Id);

        return volunteer.Id;
    }
}