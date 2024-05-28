using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetPhoto;

public record GetVolunteerPhotoResponse(IEnumerable<VolunteerPhotoDto> PhotoDto);