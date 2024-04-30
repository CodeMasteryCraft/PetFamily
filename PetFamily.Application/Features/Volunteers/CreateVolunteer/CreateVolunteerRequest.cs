using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string Name,
    string Description,
    int YearsExperience,
    int? NumberOfPetsFoundHome,
    string? DonationInfo,
    bool FromShelter,
    List<SocialMediaDto>? SocialMedias);