namespace PetFamily.Application.Dtos;

public record VolunteerPhotoDto(Guid Id, string Path, bool IsMain = true);
