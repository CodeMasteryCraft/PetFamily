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
        var volunteer = await _readDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == request.VolunteerId, cancellationToken: ct);

        if (volunteer is null)
            return Errors.General.NotFound(request.VolunteerId);

        var pets = volunteer.Pets.Select(pet => new PetDto(
            pet.Id,
            pet.Nickname,
            pet.Description,
            pet.BirthDate,
            pet.Breed,
            pet.Color,
            pet.Castration,
            pet.PeopleAttitude,
            pet.AnimalAttitude, 
            pet.OnlyOneInFamily, 
            pet.Health, 
            pet.Height, 
            [],
            pet.CreatedDate)).ToList();
      
        return new GetVolunteerPetsResponse(pets);
    }
}
