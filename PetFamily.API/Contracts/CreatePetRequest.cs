using PetFamily.Domain.ValueObjects;

namespace PetFamily.API.Contracts;

public record CreatePetRequest(
     string Nickname,
        string Description,
        DateTime BirthDate,
        string Breed,
        string Color,
        Address Address,
        Place Place,
        bool Castration,
        string PeopleAttitude,
        string AnimalAttitude,
        bool OnlyOneInFamily,
        string Health,
        int? Height,
        Weight Weight,
        bool Vaccine,
        PhoneNumber ContactPhoneNumber,
        PhoneNumber VolunteerPhoneNumber,
        bool OnTreatment
    );