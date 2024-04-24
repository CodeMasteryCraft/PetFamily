using PetFamily.Application.Dtos;

namespace PetFamily.Application.Pets.GetPets;

public record GetPetsResponse(IEnumerable<PetDto> pets, int TotalCount);