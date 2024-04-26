using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteer;

public interface IVolunteerRepository
{
    Task Add(Domain.Entities.Volunteer volunteer, CancellationToken ct);
    Task<Result<Domain.Entities.Volunteer, Error>> GetById(Guid id, CancellationToken ct);
    Task<Result<Guid, Error>> Save(Domain.Entities.Volunteer volunteer, CancellationToken ct);
}