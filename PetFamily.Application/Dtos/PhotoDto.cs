using PetFamily.Domain.Common;

namespace PetFamily.Application.Dtos;

public class PhotoDto : Entity
{
    public string Path { get; init; } = string.Empty;
    public bool IsMain { get; init; }
    public Guid? PetId { get; init; }
    public Guid? VolunteerId { get; init; }
}