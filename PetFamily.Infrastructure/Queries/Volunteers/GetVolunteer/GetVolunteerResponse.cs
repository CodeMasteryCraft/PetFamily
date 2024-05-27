using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetVolunteer;
public record GetVolunteerResponse(VolunteerDto Volunteer);