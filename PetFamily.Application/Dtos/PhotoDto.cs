namespace PetFamily.Application.Dtos;

public class PhotoDto
{
    public Guid Id { get; init; }
    public string Path { get; init; } = string.Empty;
    public bool IsMain { get; init; }
    public Guid PetId { get; init; } = Guid.Empty;
    public Guid VolunteerId { get; init; } = Guid.Empty;
}