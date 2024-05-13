namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;
    
    public IReadOnlyList<VolunteerPhotoDto> Photos { get; init; } = [];
}