using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Dtos;

public record VolunteerDto(Guid Id, string FistName, string LastName, string? Patronymic, IReadOnlyList<VolunteerPhotoDto> Photos);