using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
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
        await _volunteersRepository.Save(ct);

        return volunteer.Id;
    }
}