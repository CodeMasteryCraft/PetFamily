using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Pets.GetPets;

public record GetPetsResponse(IEnumerable<PetDto> pets, int TotalCount);