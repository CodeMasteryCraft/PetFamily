using CSharpFunctionalExtensions;
using PetFamily.Application.Features.Pets;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class PetsesRepository : IPetsRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public PetsesRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Pet, Error>> GetById(Guid id, CancellationToken ct)
    {
        var pet = await _dbContext.Pets.FindAsync(id);

        if (pet is null)
            return Errors.General.NotFound(id);

        return pet;
    }

    public Task<Result<Guid, Error>> Save(Pet pet, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}