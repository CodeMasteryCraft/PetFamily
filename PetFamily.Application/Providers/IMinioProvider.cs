using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Minio.DataModel;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Providers;

public interface IMinioProvider
{
    Task<Result<string, Error>> UploadPhoto(IFormFile photo, string path, CancellationToken ct);
    Task<Result<bool, Error>> RemovePhoto(string path, CancellationToken ct);
    Task<Result<IReadOnlyList<string>, Error>> GetPhotos(IEnumerable<string> pathes, CancellationToken ct);
    IObservable<Item> GetObjectsList(CancellationToken ct);
}