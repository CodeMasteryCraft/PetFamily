using Contracts.Requests;
using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application;

public class PetsService
{
    private readonly IPetsRepository _petsRepository;

    public PetsService(IPetsRepository petsRepository)
    {
        _petsRepository = petsRepository;
    }

    public async Task<Result<Guid, Error>> CreatePet(
        CreatePetRequest petRequest, 
        CreatePhotoRequest photoRequest, 
        CreateVaccinationRequest vaccinationRequest, 
        CancellationToken ct)
    {
        var address = Address.Create(petRequest.City, petRequest.Street, petRequest.Building, petRequest.Index);
        if (address.IsFailure)
            return address.Error;

        var place = Place.Create(petRequest.Place);
        if (place.IsFailure)
            return place.Error;

        var weight = Weight.Create(petRequest.Weight);
        if (weight.IsFailure)
            return weight.Error;

        var contactPhoneNumber = PhoneNumber.Create(petRequest.ContactPhoneNumber);
        if (contactPhoneNumber.IsFailure)
            return contactPhoneNumber.Error;

        var volunteerPhoneNumber = PhoneNumber.Create(petRequest.VolunteerPhoneNumber);
        if (volunteerPhoneNumber.IsFailure)
            return volunteerPhoneNumber.Error;

        var photo = Photo.Create(
            Guid.NewGuid(),
            photoRequest.Path,
            photoRequest.IsMain
            );

        var vaccination = Vaccination.Create(
            Guid.NewGuid(),
            vaccinationRequest.Name,
            vaccinationRequest.Applied
            );

        var pet = Pet.Create(
            Guid.NewGuid(),
            petRequest.Nickname,
            petRequest.Description,
            petRequest.BirthDate,
            petRequest.Breed,
            petRequest.Color,
            address.Value,
            place.Value,
            petRequest.Castration,
            petRequest.PeopleAttitude,
            petRequest.AnimalAttitude,
            petRequest.OnlyOneInFamily,
            petRequest.Health,
            petRequest.Height,
            weight.Value,
            contactPhoneNumber.Value,
            volunteerPhoneNumber.Value,
            petRequest.OnTreatment,
            DateTimeOffset.Now
            );

        var idResult = await _petsRepository.Add(pet.Value, ct);

        if (idResult.IsFailure)
            return idResult.Error;
        
        return idResult;
    }
}