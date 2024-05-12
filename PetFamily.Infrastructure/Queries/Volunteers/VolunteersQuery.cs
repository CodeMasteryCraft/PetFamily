using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers;

public class VolunteersQuery : IVolunteersQuery
{
    private readonly PetFamilyReadDbContext _dbContext;

    public VolunteersQuery(PetFamilyReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<VolunteerDto, Error>> GetById(Guid id, CancellationToken ct)
    {
        var volunteerDto = await  _dbContext.Volunteers
            .Include(v => v.Photos)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

        if (volunteerDto is null)
            return Errors.General.NotFound(id);

        return volunteerDto;
    }
}