using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;
using System.Dynamic;

namespace PetFamily.Domain.Entities;

public class Pet
{
    //почему я задал именно такое ограничене по длине?
    //чекнул на сайте https://text.ru/seo примерный объем текста,
    //и понял, что данное количество символов достаточно для данных строк
    public const int MAX_NAME_LENGTH = 100;
    public const int MAX_NAME_DESCRIPTION = 1500;
    public const int MAX_NAME_BREED = 50;
    public const int MAX_NAME_COLOR = 50;
    public const int MAX_NAME_ATTITUBE = 750;
    public const int MAX_NAME_HEALTH = 750;

    private Pet(
        Guid id,
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
        DateTimeOffset createdDate)
    {
        Id = id;
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
    }

    public Guid Id { get; private set; }

    public string Nickname { get; private set; }
    public string Description { get; private set; }
    public string Breed { get; private set; }
    public string Color { get; private set; }
    public string PeopleAttitude { get; private set; }
    public string AnimalAttitude { get; private set; }
    public string Health { get; private set; }

    public Address Address { get; private set; }
    public Place Place { get; private set; }
    public Weight Weight { get; private set; }
    public PhoneNumber ContactPhoneNumber { get; private set; }
    public PhoneNumber VolunteerPhoneNumber { get; private set; }

    public bool Castration { get; private set; }
    public bool OnlyOneInFamily { get; private set; }
    public bool OnTreatment { get; private set; }

    public int? Height { get; private set; }

    public DateTimeOffset BirthDate { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }

    public IReadOnlyList<Vaccination> Vaccinations => _vaccinations;
    private readonly List<Vaccination> _vaccinations = [];

    public IReadOnlyList<Photo> Photos => _photos;
    private readonly List<Photo> _photos = [];

    public static Result<Pet, Error> Create(
        Guid id,
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
        DateTimeOffset createdDate)
    {
        nickname = nickname.Trim();
        description = description.Trim();
        breed = breed.Trim();
        color = color.Trim();
        peopleAttitude = peopleAttitude.Trim();
        animalAttitude = animalAttitude.Trim();
        health = health.Trim();

        if (nickname.Length is < 1 or > MAX_NAME_LENGTH)
            return Errors.General.InvalidLength(nickname);

        if (description.Length is < 1 or > MAX_NAME_DESCRIPTION)
            return Errors.General.InvalidLength(description);

        if (birthDate > DateTimeOffset.UtcNow)
            return Errors.General.DateIsInvalid(birthDate);

        if (breed.Length is < 1 or > MAX_NAME_BREED)
            return Errors.General.InvalidLength(breed);

        if (color.Length is < 1 or > MAX_NAME_COLOR)
            return Errors.General.InvalidLength(color);

        if (peopleAttitude.Length is < 1 or > MAX_NAME_ATTITUBE)
            return Errors.General.InvalidLength(peopleAttitude);

        if (animalAttitude.Length is < 1 or > MAX_NAME_ATTITUBE)
            return Errors.General.InvalidLength(animalAttitude);

        if (health.Length is < 1 or > MAX_NAME_HEALTH)
            return Errors.General.InvalidLength(health);


        return new Pet(
            id,
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
            createdDate);
    }
}