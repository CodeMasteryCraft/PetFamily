using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Pets.CreateVaccination;

public class CreateVaccinationService
{
    private readonly IPetRepository _petRepository;

    public CreateVaccinationService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateVaccinationRequest request, CancellationToken ct)
    {
        var pet = await _petRepository.GetById(request.PetId, ct);
        if (pet.IsFailure)
            return pet.Error;

        var vaccinations = request.Vaccinations
            .Select(s =>
            {
                var vac = Vaccination.Create(s.Name, s.Applied).Value;
                return  new Vaccination(vac.Name, vac.Applied);
                
            });

        

        pet.Value.PublishVaccination(vaccinations);

        return await _petRepository.Save(pet.Value, ct);
    }
}