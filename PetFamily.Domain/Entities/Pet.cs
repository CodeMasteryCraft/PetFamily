using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class Pet : Entity
{
    public const int PHOTO_COUNT_LIMIT = 25;
    public const int PHOTO_COUNT_MIN = 1;

    private Pet()
    {
    }

    private Pet(
        string nickname,
        string description,
        DateTimeOffset birthDate,
        string breed,
        string color,
        Address address,
        Place place,
        bool castration,
        string peopleAttitude,
        string animalAttitude,
        bool onlyOneInFamily,
        string health,
        int? height,
        Weight weight,
        PhoneNumber contactPhoneNumber,
        PhoneNumber volunteerPhoneNumber,
        bool onTreatment,
        DateTimeOffset createdDate,
        IEnumerable<Vaccination> vaccinations,
        List<PetPhoto> photos)
    {
        Nickname = nickname;
        Description = description;
        BirthDate = birthDate;
        Breed = breed;
        Color = color;
        Address = address;
        Place = place;
        Castration = castration;
        PeopleAttitude = peopleAttitude;
        AnimalAttitude = animalAttitude;
        OnlyOneInFamily = onlyOneInFamily;
        Health = health;
        Height = height;
        Weight = weight;
        ContactPhoneNumber = contactPhoneNumber;
        VolunteerPhoneNumber = volunteerPhoneNumber;
        OnTreatment = onTreatment;
        CreatedDate = createdDate;
        _vaccinations = vaccinations.ToList();
        _photos = photos;
    }

    public string Nickname { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Breed { get; private set; } = null!;
    public string Color { get; private set; } = null!;
    public string PeopleAttitude { get; private set; } = null!;
    public string AnimalAttitude { get; private set; } = null!;
    public string Health { get; private set; } = null!;

    public Address Address { get; private set; } = null!;
    public Place Place { get; private set; } = null!;
    public Weight Weight { get; private set; } = null!;
    public PhoneNumber ContactPhoneNumber { get; private set; } = null!;
    public PhoneNumber VolunteerPhoneNumber { get; private set; } = null!;

    public bool Castration { get; private set; }
    public bool OnlyOneInFamily { get; private set; }
    public bool OnTreatment { get; private set; }

    public int? Height { get; private set; }

    public DateTimeOffset BirthDate { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }

    public IReadOnlyList<Vaccination> Vaccinations => _vaccinations;
    private readonly List<Vaccination> _vaccinations = [];

    public IReadOnlyList<PetPhoto> Photos => _photos;
    private readonly List<PetPhoto> _photos = [];

    public static Result<Pet, Error> Create(
        string nickname,
        string description,
        DateTimeOffset birthDate,
        string breed,
        string color,
        Address address,
        Place place,
        bool castration,
        string peopleAttitude,
        string animalAttitude,
        bool onlyOneInFamily,
        string health,
        int height,
        Weight weight,
        PhoneNumber contactPhoneNumber,
        PhoneNumber volunteerPhoneNumber,
        bool onTreatment,
        IEnumerable<Vaccination> vaccinations,
        IEnumerable<PetPhoto> photos)
    {
        breed = breed.Trim();
        color = color.Trim();
        peopleAttitude = peopleAttitude.Trim();
        animalAttitude = animalAttitude.Trim();

        if (nickname.IsEmpty() || nickname.Length > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (description.IsEmpty() || description.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (birthDate > DateTimeOffset.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(birthDate.Year));

        if (breed.IsEmpty() || breed.Length > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (color.IsEmpty() || color.Length > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (peopleAttitude.IsEmpty() || peopleAttitude.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (animalAttitude.IsEmpty() || animalAttitude.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (health.IsEmpty() || health.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (height <= 0)
            return Errors.General.ValueIsInvalid(nameof(height));

        var photosList = photos.ToList();
        if (photosList.Count is > PHOTO_COUNT_LIMIT or < PHOTO_COUNT_MIN)
            return Errors.Pets.PhotoCountLimit();

        return new Pet(
            nickname,
            description,
            birthDate,
            breed,
            color,
            address,
            place,
            castration,
            peopleAttitude,
            animalAttitude,
            onlyOneInFamily,
            health,
            height,
            weight,
            contactPhoneNumber,
            volunteerPhoneNumber,
            onTreatment,
            DateTimeOffset.UtcNow,
            vaccinations,
            photosList);
    }
}