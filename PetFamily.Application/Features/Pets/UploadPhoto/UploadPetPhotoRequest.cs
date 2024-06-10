using Microsoft.AspNetCore.Http;

namespace PetFamily.Application.Features.Pets.UploadPhoto;

public record UploadPetPhotoRequest(Guid PetId, IFormFile File, bool IsMain);