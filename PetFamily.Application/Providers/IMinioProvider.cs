using Microsoft.AspNetCore.Http;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Providers;

public interface IMinioProvider
{
    Task<Result<string>> UploadPhoto(IFormFile photo, string path);
    Task<Result<bool>> RemovePhoto(string path);
    Task<Result<IReadOnlyList<string>>> GetPhotos(IEnumerable<string> pathes);
}