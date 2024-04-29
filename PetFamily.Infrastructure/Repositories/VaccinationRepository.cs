using CSharpFunctionalExtensions;
using PetFamily.Application.Features.Vaccinations;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class VaccinationRepository : IVaccinationRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public VaccinationRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Vaccination, Error>> GetById(Guid id, CancellationToken ct)
    {
        var vac = await _dbContext.Vaccinations.FindAsync(id);

        if (vac is null)
            return Errors.General.NotFound(id);

        return vac;
    }
}