namespace PetFamily.Infrastructure.ReadModels;

public class VolunteerReadModel
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Patronymic { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int YearsExperience { get; init; }
    public int? NumberOfPetsFoundHome { get; init; }
    public string DonationInfo { get; init; } = string.Empty;
    public bool FromShelter { get; init; }
    public List<VolunteerPhotoReadModel> Photos { get; init; } = [];
    public ICollection<SocialMediaReadModel> SocialMedias { get; init; } = [];
    public ICollection<PetReadModel> Pets { get; init; } = [];
}