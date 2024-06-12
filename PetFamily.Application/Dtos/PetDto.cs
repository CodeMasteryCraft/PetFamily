namespace PetFamily.Application.Dtos;

public record PetDto(
    Guid Id,
    string Nickname,
    string Description,
    string City,
    string Street,
    string Building,
    string Index,
    string ContactPhoneNumber,
    DateTimeOffset CreatedDate);