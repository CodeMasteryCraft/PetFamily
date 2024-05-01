using Microsoft.AspNetCore.Http;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public record UploadVolunteerPhotoRequest(Guid VolunteerId, IFormFile File);