using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Repositories;

public class PetRepository : IPetsRepository
{
    private readonly PetFamilyDbContext _dbContext;

    public PetRepository(PetFamilyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, IReadOnlyList<Error>>> Add(Pet pet, CancellationToken ct)
    {
        await _dbContext.AddAsync(pet, ct);
        
        var result = await _dbContext.SaveChangesAsync(ct);

        if (result == 0)
            return new[] { new Error("record.saving", "Pet can not be save") };
        
        return pet.Id;
    }
}