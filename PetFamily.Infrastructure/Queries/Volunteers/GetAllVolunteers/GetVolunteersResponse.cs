using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers;
public record GetVolunteersResponse(List<VolunteerDto> Volunteers);