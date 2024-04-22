using Contracts.Pets.Dtos;

namespace Contracts.Pets.Responses;

public record GetPetsByPageResponse(IEnumerable<PetDto> pets);