using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets.CreateVaccination;

public class CreateVaccinationService
{
    private readonly IPetsRepository _petsRepository;

    public CreateVaccinationService(IPetsRepository petsRepository)
    {
        _petsRepository = petsRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateVaccinationRequest request, CancellationToken ct)
    {
        var pet = await _petsRepository.GetById(request.PetId, ct);
        if (pet.IsFailure)
            return pet.Error;

        var vaccinations = request.Vaccinations
            .Select(s =>
            {
                var vac = Vaccination.Create(s.Name, s.Applied).Value;
                return  new Vaccination(vac.Name, vac.Applied);
                
            });

        

        pet.Value.AddVaccination(vaccinations);

        return await _petsRepository.Save(pet.Value, ct);
    }
}