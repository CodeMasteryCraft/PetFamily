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

    public async Task<Result<Guid, ResultEvent>> Handle(CreateVolunteerRequest request, CancellationToken ct)
    {
        //TODO
        var socialMedias = request.SocialMedias?
            .Select(s =>
            {
                var social = Social.Create(s.Social).Value;
                return new SocialMedia(s.Link, social);
            }) ?? [];

        var volunteer = new Volunteer(
            request.Name,
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