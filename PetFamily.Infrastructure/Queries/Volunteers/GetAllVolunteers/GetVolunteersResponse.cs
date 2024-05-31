using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers;
public record GetVolunteersResponse(List<VolunteerDto> Volunteers);