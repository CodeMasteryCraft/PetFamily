using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
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

    public async Task<Result<Guid, Error>> Add(Pet pet, CancellationToken ct)
    {
        await _dbContext.AddAsync(pet, ct);

        var result = await _dbContext.SaveChangesAsync(ct);

        if (result == 0)
            return new Error("record.saving", "Pet can not be save");

        return pet.Id;
    }

    public async Task<Result<Pet, Error>> GetById(Guid id)
    {
        var pet = await _dbContext.Pets.FindAsync(id);

        if (pet is null)
            return Errors.General.NotFound(id);

        return pet;
    }

    public async Task<IReadOnlyList<Pet>> GetByPage(int page, int size, CancellationToken ct)
    {
        return await _dbContext.Pets
            .AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Pet>> GetByFilter(GetPetsFilter filter)
    {
        var query = _dbContext.Pets.AsNoTracking();

        if (string.IsNullOrWhiteSpace(filter.Nickname) == false)
        {
            query = query.Where(p => p.Nickname.Contains(filter.Nickname));
        }

        if (string.IsNullOrWhiteSpace(filter.Breed) == false)
        {
            query = query.Where(p => p.Nickname.Contains(filter.Breed));
        }

        if (string.IsNullOrWhiteSpace(filter.Color) == false)
        {
            query = query.Where(p => p.Nickname.Contains(filter.Color));
        }

        var pets = await query.ToListAsync();

        return pets;
    }

    public class GetPetsFilter
    {
        public string? Nickname { get; set; }
        public string? Breed { get; set; }
        public string? Color { get; set; }
    }
}