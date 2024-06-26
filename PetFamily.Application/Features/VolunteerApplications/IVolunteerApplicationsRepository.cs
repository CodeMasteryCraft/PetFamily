using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.VolunteerApplications;

public interface IVolunteerApplicationsRepository
{
    Task<Result<VolunteerApplication>> GetById(Guid id, CancellationToken ct);
    Task Add(VolunteerApplication application, CancellationToken ct);
}