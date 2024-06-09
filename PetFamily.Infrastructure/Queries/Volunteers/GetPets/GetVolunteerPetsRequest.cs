namespace PetFamily.Infrastructure.Queries.Volunteers.GetPets;

public record GetVolunteerPetsRequest(Guid VolunteerId, int Size = 9, int Page = 1);