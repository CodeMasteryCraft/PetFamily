using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;

public class GetVolunteersQuery
{
    private readonly PetFamilyReadDbContext _dbContext;

    public GetVolunteersQuery(PetFamilyReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetVolunteersResponse> Handle()
    {
        var volunteers = await _dbContext.Volunteers.ToListAsync();

        var volunteersDtos = volunteers.Select(v =>
            new VolunteerDto(v.Id, v.FirstName, v.LastName, v.Patronymic, []));

        return new(volunteersDtos);
    }
}