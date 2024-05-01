using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Abstractions;

public interface IMinioService
{
    Task<Result<string, Error>> UploadPhoto(Stream photoStream, string path);
}