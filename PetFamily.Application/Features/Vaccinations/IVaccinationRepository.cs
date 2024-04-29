using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Vaccinations;

public interface IVaccinationRepository
{
    Task<Result<Vaccination, Error>> GetById(Guid id, CancellationToken ct);
}