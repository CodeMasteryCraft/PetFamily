using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Abstractions;

public interface IMinioProvider
{
    Task<Result<string, Error>> UploadPhoto(IFormFile photo, string path, CancellationToken ct);
    Task<Result<string, Error>> RemovePhoto(string path, CancellationToken ct);
    Task<Result<string, Error>> GetPhoto(string pathes);
    Task<Result<IReadOnlyList<string>, Error>> GetPhotos(List<string> paths, CancellationToken ct);
}