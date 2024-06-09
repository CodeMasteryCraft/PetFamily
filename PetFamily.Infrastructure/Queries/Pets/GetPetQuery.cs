using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Pets;

public class GetPetQuery
{
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetPetQuery(PetFamilyReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<GetPetResponse>> Handle(GetPetRequest request, CancellationToken ct)
    {
        var pet = await _readDbContext.Pets.FirstOrDefaultAsync(p => p.Id == request.PetId, cancellationToken: ct);

        if (pet is null)
            return Errors.General.NotFound(request.PetId);
        
        var petDto = new PetDto(pet.Id, pet.Nickname, pet.Description, pet.BirthDate, pet.Breed, pet.Color,
            pet.Castration, pet.PeopleAttitude, pet.AnimalAttitude, pet.OnlyOneInFamily, pet.Health, pet.Height, [],
            pet.CreatedDate);

        return new GetPetResponse(petDto);
    }
}