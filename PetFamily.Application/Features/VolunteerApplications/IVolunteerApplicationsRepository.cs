using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.VolunteerApplications;

public interface IVolunteerApplicationsRepository
{
    Task<Result<VolunteerApplication, Error>> GetById(Guid id, CancellationToken ct);
}