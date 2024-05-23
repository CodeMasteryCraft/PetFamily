using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers;

public class GetVolunteersQuery
{
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteersQuery(PetFamilyReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<GetVolunteersResponse, Error>> Handle(int size, int page, CancellationToken ct)
    {
        var volunteers = await _readDbContext.Volunteers
            .Include(v => v.Pets)
            .Include(v => v.Photos)
            .ToListAsync(cancellationToken: ct);
        if (volunteers.Count == 0)
            return Errors.General.NotFound();
        if (size != 0 && page != 0)
        {
            var volunteersWithPagination = await _readDbContext.Volunteers
                .Include(v => v.Pets)
                .Include(v => v.Photos)
                .Skip(size*page)
                .Take(size)
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);
            if (volunteers.Count == 0)
                return Errors.General.NotFound();
            return new GetVolunteersResponse(volunteersWithPagination);
        }
        return new GetVolunteersResponse(volunteers);
    }
}