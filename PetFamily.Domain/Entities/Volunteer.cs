using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class Volunteer : Entity
{
    public const int PHOTO_COUNT_LIMIT = 5;

    private Volunteer()
    {
    }

    public Volunteer(
        string name,
        string description,
        int yearsExperience,
        int? numberOfPetsFoundHome,
        string? donationInfo,
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

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int YearsExperience { get; private set; }
    public int? NumberOfPetsFoundHome { get; private set; }
    public string? DonationInfo { get; private set; }
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

    public Result<bool, Error> AddPhoto(Photo photo)
    {
        if (_photos.Count >= PHOTO_COUNT_LIMIT)
        {
            return Errors.Volunteers.PhotoCountLimit();
        }

        _photos.Add(photo);
        return true;
    }
    
    public Result<bool, Error> DeletePhoto(string path)
    {
        var photo = _photos.FirstOrDefault(p => p.Path.Contains(path));
        if (photo is null)
            return Errors.General.NotFound();
        
        _photos.Remove(photo);
        return true;
    }

    public static Result<Volunteer, Error> Create(
        string name,
        string description,
        int yearsExperience,
        int? numberOfPetsFoundHome,
        string? donationInfo,
        bool fromShelter,
        IEnumerable<SocialMedia> socialMedias)
    {
        if (name.IsEmpty() || name.Length > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(name));

        if (description.IsEmpty() || description.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(description));

        if (yearsExperience < 0)
            return Errors.General.ValueIsInvalid(nameof(yearsExperience));

        if (numberOfPetsFoundHome < 0)
            return Errors.General.ValueIsInvalid(nameof(numberOfPetsFoundHome));

        if (donationInfo?.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(donationInfo));

        return new Volunteer(
            name,
            description,
            yearsExperience,
            numberOfPetsFoundHome,
            donationInfo,
            fromShelter,
            socialMedias);
    }
}