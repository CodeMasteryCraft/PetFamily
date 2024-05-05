namespace PetFamily.Application.Features.Volunteers.DeletePhoto;

public record DeleteVolunteerPhotoRequest(
    Guid VolunteerId,
    string Path
    );