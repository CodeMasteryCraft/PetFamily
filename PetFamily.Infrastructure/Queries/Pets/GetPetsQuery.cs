using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Pets.GetPets;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Pets;

public class GetPetsQuery
{
    private readonly PetFamilyReadDbContext _dbContext;

    public GetPetsQuery(PetFamilyReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task<GetPetsResponse> Handle(GetPetsRequest request, CancellationToken ct)
    // {
    //     var petsQuery = _dbContext.Pets
    //         .Where(p => string.IsNullOrWhiteSpace(request.Nickname) || p.Nickname.Contains(request.Nickname))
    //         .Where(p => string.IsNullOrWhiteSpace(request.Color) || p.Nickname.Contains(request.Color))
    //         .Where(p => string.IsNullOrWhiteSpace(request.Breed) || p.Nickname.Contains(request.Breed))
    //         .OrderBy(p => p.CreatedDate);
    //
    //     var totalCount = await petsQuery.CountAsync(cancellationToken: ct);
    //
    //     var pets = await petsQuery
    //         .Skip((request.Page - 1) * request.Size)
    //         .Take(request.Size)
    //         .ToListAsync(cancellationToken: ct);
    //
    //     return new(pets, totalCount);
    // }
}