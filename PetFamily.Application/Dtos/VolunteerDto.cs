namespace PetFamily.Application.Dtos;

public record VolunteerDto(Guid Id, string Name, IReadOnlyList<VolunteerPhotoDto> Photos);