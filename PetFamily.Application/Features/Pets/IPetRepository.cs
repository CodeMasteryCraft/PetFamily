using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets;

public interface IPetRepository
{
    Task<Result<Pet, Error>> GetById(Guid id, CancellationToken ct);
    Task<Result<Guid, Error>> Save(Pet pet, CancellationToken ct);
}