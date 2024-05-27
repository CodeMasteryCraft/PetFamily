namespace PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers;

public record GetVolunteersRequest(int Size = 10, int Page = 1);
