using Microsoft.AspNetCore.Http;
using Minio.DataModel;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Providers;

public interface IMinioProvider
{
    Task<Result<string>> UploadPhoto(Stream stream, string path, CancellationToken ct);
    Task<Result> RemovePhoto(string path, CancellationToken ct);
    Task<Result<IReadOnlyList<string>>> GetPhotos(IEnumerable<string> pathes, CancellationToken ct);
    IObservable<Item> GetObjectsList(CancellationToken ct);
    Task<Result> RemovePhotos(List<string> paths, CancellationToken ct);
}