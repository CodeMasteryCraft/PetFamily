using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Repositories
{
    public interface IPhotoRepository
    {
        Task<Result<Photo, ResultEvent>> GetById(Guid id, CancellationToken ct);
    }
}