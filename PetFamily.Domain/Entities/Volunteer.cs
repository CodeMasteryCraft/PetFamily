namespace PetFamily.Domain.Entities;

public class Volunteer
{
    private Volunteer()
    {
    }

    public Volunteer(
        string name,
        string description,
        int years,
        int petFoundHome,
        string donationInfo,
        bool fromShelter,
        Photo mainPhoto)
    {
        Name = name;
        Description = description;
        Years = years;
        PetFoundHome = petFoundHome;
        DonationInfo = donationInfo;
        FromShelter = fromShelter;
        MainPhoto = mainPhoto;
    }

    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Years { get; private set; }
    public int PetFoundHome { get; private set; }
    public string DonationInfo { get; private set; }
    public bool FromShelter { get; private set; }
    public Photo MainPhoto { get; private set; }

    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
    private readonly List<SocialMedia> _socialMedias = [];

    public IReadOnlyList<Pet> Pets => _pets;
    private readonly List<Pet> _pets = [];

    public void PublishPet(Pet pet)
    {
        _pets.Add(pet);
    }
}