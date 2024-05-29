using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers;

public interface IVolunteersRepository
{
    Task Add(Volunteer volunteer, CancellationToken ct);
    Task<Result<Volunteer>> GetById(Guid id, CancellationToken ct);
}