using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers;

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

    public async Task<Result<GetVolunteersResponse>> Handle(GetVolunteersRequest request, CancellationToken ct)
    {
        return await _cacheProvider.GetOrSetAsync(
            CacheKeys.Volunteers,
            async () =>
            {
                var volunteers = await _dbContext.Volunteers
                    .Include(v => v.Pets)
                    .Include(v => v.Photos)
                    .Skip(request.Size * (request.Page - 1))
                    .Take(request.Size)
                    .ToListAsync(cancellationToken: ct);


                var volunteersDtos = volunteers.Select(v => new VolunteerDto(
                    v.Id,
                    v.FirstName,
                    v.LastName,
                    v.Patronymic,
                    v.Photos.Select(x => new VolunteerPhotoDto { Id = x.Id, Path = x.Path, IsMain = x.IsMain })
                        .ToList())).ToList();

                return new GetVolunteersResponse(volunteersDtos);
            },
            ct) ?? new([]);
    }
}