using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerService
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerService(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken ct)
    {
        var socialMedias = request.SocialMedias
            .Select(s =>
            {
                var social = Social.Create(s.Social).Value;
                return new SocialMedia(s.Link, social);
            });

        var volunteer = new Volunteer(
            request.Name,
            request.Description,
            request.YearsExperience,
            request.NumberOfPetsFoundHome,
            request.DonationInfo,
            request.FromShelter,
            socialMedias);

        await _volunteerRepository.Add(volunteer, ct);
        return await _volunteerRepository.Save(volunteer, ct);
    }
}