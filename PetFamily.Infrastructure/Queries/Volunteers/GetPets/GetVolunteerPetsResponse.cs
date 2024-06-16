using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPets;

public record GetVolunteerPetsResponse(IEnumerable<PetDto> Pets);