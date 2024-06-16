using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Dtos;

public record PetDto(
    Guid Id,
    string Nickname,
    string Description,
    DateTimeOffset BirthDate,
    string Breed,
    string Color,
    Address Address,
    bool Castration,
    string PeopleAttitude,
    string AnimalAttitude,
    bool OnlyOneInFamily,
    string Health,
    int? Height,
    //float Weight,
    //string ContactPhoneNumber,
    //string VolunteerPhoneNumber,
    IReadOnlyList<PetPhotoDto> Photos,
    DateTimeOffset CreatedDate);
