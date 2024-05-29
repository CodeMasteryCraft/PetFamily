using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;

public record GetVolunteersResponse(IEnumerable<VolunteerDto> Volunteers);