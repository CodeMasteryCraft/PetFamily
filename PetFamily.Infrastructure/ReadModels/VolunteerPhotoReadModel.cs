namespace PetFamily.Infrastructure.ReadModels;

public class VolunteerPhotoReadModel
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string Path { get; init; } = string.Empty;
    public bool IsMain { get; init; }
}