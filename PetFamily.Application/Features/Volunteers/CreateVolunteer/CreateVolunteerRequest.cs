using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string Description,
    int YearsExperience,
    int? NumberOfPetsFoundHome,
    string? DonationInfo,
    bool FromShelter,
    List<SocialMediaDto>? SocialMedias);