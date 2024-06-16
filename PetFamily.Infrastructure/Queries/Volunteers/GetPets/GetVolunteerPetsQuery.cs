using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPets;

public class GetVolunteerPetsQuery
{
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetVolunteerPetsQuery(PetFamilyReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<GetVolunteerPetsResponse>> Handle(
        GetVolunteerPetsRequest request, 
        CancellationToken ct)
    {
       
        var petReadModel = await _readDbContext.Pets
            .Where(p => p.VolunteerId == request.VolunteerId)
            .ToListAsync(cancellationToken: ct);

        var petDto = petReadModel.Select(pet => new PetDto(
            pet.Id,
            pet.Nickname,
            pet.Description,
            pet.City,
            pet.Street,
            pet.Building,
            pet.Index,
            pet.ContactPhoneNumber,
            pet.CreatedDate))
            .ToList();
      
        return new GetVolunteerPetsResponse(petDto);
    }
}
