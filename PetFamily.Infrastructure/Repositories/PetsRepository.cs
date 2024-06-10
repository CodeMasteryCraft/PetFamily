using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Pets;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class PetsRepository : IPetsRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public PetsRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<Pet>> GetById(Guid id, CancellationToken ct)
    {
        var pet = await _dbContext.Pets
            .Include(v => v.Photos)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

        if (pet is null)
            return Errors.General.NotFound(id);

        return pet;
    }
}