using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Pets.GetPetById;

public class GetByIdPetQuery
{
    private readonly PetFamilyReadDbContext _readDbContext;

    public GetByIdPetQuery(PetFamilyReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<GetByIdPetResponse>> Handle(GetByIdPetRequest request, CancellationToken ct)
    {
        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(p => p.Id == request.PetId, cancellationToken: ct);

        if (pet is null)
            return Errors.General.NotFound(request.PetId);

        var petDto = new PetDto(
            pet.Id, 
            pet.Nickname, 
            pet.Description, 
            pet.City,
            pet.Street,
            pet.Building,
            pet.Index,
            pet.ContactPhoneNumber, 
            pet.CreatedDate);

        return new GetByIdPetResponse(petDto);
    }
}