namespace PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;

public record GetVolunteersRequest(int Size = 10, int Page = 1);
