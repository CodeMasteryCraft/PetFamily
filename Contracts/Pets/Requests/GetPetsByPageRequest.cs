namespace Contracts.Pets.Requests;

public record GetPetsByPageRequest(int Size = 10, int Page = 1);