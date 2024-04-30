using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public VolunteersRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Volunteer volunteer, CancellationToken ct)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, ct);
    }

    public async Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken ct)
    {
        var result = await _dbContext.SaveChangesAsync(ct);

        if (result == 0)
            return Errors.General.SaveFailure("Volunteer");

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(Guid id, CancellationToken ct)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

        if (volunteer is null)
            return Errors.General.NotFound(id);

        return volunteer;
    }
}