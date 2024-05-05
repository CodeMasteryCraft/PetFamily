using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Abstractions;

public interface IMinioProvider
{
    Task<Result<string, ResultEvent>> UploadPhoto(IFormFile photo, string path, CancellationToken ct);
    Task<Result<ResultEvent>> RemovePhoto(string path, CancellationToken ct);
    Task<Result<string, ResultEvent>> GetPhoto(string pathes);
    Task<Result<IReadOnlyList<string>, ResultEvent>> GetPhotos(List<string> paths, CancellationToken ct);
}