using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Queries.Pets.GetPets;

public record GetPetsResponse(IEnumerable<PetDto> pets, int TotalCount);