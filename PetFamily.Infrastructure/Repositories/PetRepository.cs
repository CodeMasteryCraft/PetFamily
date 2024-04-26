using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Pets;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class PetRepository : IPetsRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public PetRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Pet, Error>> GetById(Guid id)
    {
        var pet = await _dbContext.Pets.FindAsync(id);

        if (pet is null)
            return Errors.General.NotFound(id);

        return pet;
    }
}