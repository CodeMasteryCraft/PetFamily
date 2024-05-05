using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Dtos;

public class VolunterrDto : Entity
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int YearsExperience { get; init; }
    public int? NumberOfPetsFoundHome { get; init; }
    public string? DonationInfo { get; init; }
    public bool FromShelter { get; init; }

    public IReadOnlyList<Photo> Photos { get; init; } = [];

    public IReadOnlyList<SocialMedia> SocialMedias { get; init; } = [];

    public IReadOnlyList<Pet> Pets { get; init; } = [];
}
