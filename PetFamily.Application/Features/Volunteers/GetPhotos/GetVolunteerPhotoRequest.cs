namespace PetFamily.Application.Features.Volunteers.GetPhotos;

public record GetVolunteerPhotoRequest(
    Guid VolunteerId,
    string Photo
    );
