namespace PetFamily.Domain.Entities;

public class Volunteer
{
    private Volunteer()
    {
    }

    public Volunteer(
        string name,
        string description,
        int yearsExperience,
        int numberOfPetsFoundHome,
        string donationInfo,
        bool fromShelter,
        IEnumerable<SocialMedia> socialMedias)
    {
        Name = name;
        Description = description;
        YearsExperience = yearsExperience;
        NumberOfPetsFoundHome = numberOfPetsFoundHome;
        DonationInfo = donationInfo;
        FromShelter = fromShelter;
        _socialMedias = socialMedias.ToList();
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int YearsExperience { get; private set; }
    public int NumberOfPetsFoundHome { get; private set; }
    public string DonationInfo { get; private set; }
    public bool FromShelter { get; private set; }

    public IReadOnlyList<Photo> Photos => _photos;
    private readonly List<Photo> _photos = [];

    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
    private readonly List<SocialMedia> _socialMedias = [];

    public IReadOnlyList<Pet> Pets => _pets;
    private readonly List<Pet> _pets = [];

    public void PublishPet(Pet pet)
    {
        _pets.Add(pet);
    }

    public void PublishSocialMedia(SocialMedia socialMedia)
    {
        _socialMedias.Add(socialMedia);
    }
}