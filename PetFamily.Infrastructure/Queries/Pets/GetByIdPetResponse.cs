using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Queries.Pets;

public record GetByIdPetResponse(PetDto Pet);