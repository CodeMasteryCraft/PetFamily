namespace PetFamily.Application.Dtos;

public record VolunteerDto(
    Guid Id,
    string FirstName,
    string Lastname,
    string? Patronomic,
    IReadOnlyList<VolunteerPhotoDto> Photos);