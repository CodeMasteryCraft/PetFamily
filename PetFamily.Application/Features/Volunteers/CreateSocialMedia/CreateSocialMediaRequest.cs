namespace PetFamily.Application.Features.Volunteers.CreateSocialMedia;

public record CreateSocialMediaRequest(
    Guid VolunteerId,
    string Link,
    string Social);