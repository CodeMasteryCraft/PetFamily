namespace Contracts.Pets.Dtos;

public record PetDto(
    Guid Id,
    string Nickname,
    string Destription,
    string City,
    string Street,
    string Building,
    string Index,
    string ContactPhoneNumber);