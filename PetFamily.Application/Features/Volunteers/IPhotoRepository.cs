using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Repositories
{
    public interface IPhotoRepository
    {
        Task<Result<Photo, Error>> GetById(Guid id, CancellationToken ct);
    }
}