using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets;

public interface IPetsRepository
{
    Task<Result<Pet>> GetById(Guid id, CancellationToken ct);
}