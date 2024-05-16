using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Providers;

public interface IMinioProvider
{
    Task<Result<string, Error>> UploadPhoto(IFormFile photo, string path);
    Task<Result<bool, Error>> RemovePhoto(string path);
    Task<Result<IReadOnlyList<string>, Error>> GetPhotos(IEnumerable<string> pathes);
}