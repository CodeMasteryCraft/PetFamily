using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets.CreateVaccination;

public record CreateVaccinationRequest(
    Guid PetId,
    List<Vaccination> Vaccinations);