using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers;

public interface IVolunteerRepository
{
    Task Add(Volunteer volunteer, CancellationToken ct);
    Task<Result<Volunteer, Error>> GetById(Guid id, CancellationToken ct);
    Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken ct);
}