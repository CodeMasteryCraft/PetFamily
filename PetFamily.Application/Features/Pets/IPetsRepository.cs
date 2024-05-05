using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets;

public interface IPetsRepository
{
    Task<Result<Pet, ResultEvent>> GetById(Guid id, CancellationToken ct);
    Task<Result<Guid, ResultEvent>> Save(Pet pet, CancellationToken ct);
}