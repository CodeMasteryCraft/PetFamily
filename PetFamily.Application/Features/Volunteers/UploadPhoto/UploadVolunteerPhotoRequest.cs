using Microsoft.AspNetCore.Http;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

<<<<<<< Updated upstream
public record UploadVolunteerPhotoRequest(Guid VolunteerId, IFormFile File);
=======
public record UploadVolunteerPhotoRequest(Guid VolunteerId, IFormFile File, bool IsMain);
>>>>>>> Stashed changes
