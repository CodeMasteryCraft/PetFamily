using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;

public class GetVolunteersQuery
{
    private readonly PetFamilyReadDbContext _dbContext;
    private readonly ICacheProvider _cacheProvider;

    public GetVolunteersQuery(PetFamilyReadDbContext dbContext, ICacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cacheProvider = cacheProvider;
    }

    public async Task<GetVolunteersResponse> Handle(CancellationToken ct)
    {
        return await _cacheProvider.GetOrSetAsync(
            CacheKeys.Volunteers,
            async () =>
            {
                var volunteers = await _dbContext.Volunteers.ToListAsync(ct);

                var volunteersDtos = volunteers.Select(v =>
                    new VolunteerDto(v.Id, v.FirstName, v.LastName, v.Patronymic, []));

                return new GetVolunteersResponse(volunteersDtos);
            },
            ct) ?? new([]);
    }
}