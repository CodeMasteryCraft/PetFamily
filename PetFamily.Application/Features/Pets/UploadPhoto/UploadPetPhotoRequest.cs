using Microsoft.AspNetCore.Http;

namespace PetFamily.Application.Features.Pets.UploadPhoto;

public record UploadPetPhotoRequest(IFormFile File, bool IsMain);