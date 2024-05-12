namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    
    public List<PhotoDto> Photos { get; init; } = [];
}