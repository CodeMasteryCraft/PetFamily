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

    public async Task<Result<Guid, Error>> CreatePet(CreatePetRequest request, CancellationToken ct)
    {
        var address = Address.Create(request.City, request.Street, request.Building, request.Index).Value;
        var place = Place.Create(request.Place).Value;
        var weight = Weight.Create(request.Weight).Value;
        var contactPhoneNumber = PhoneNumber.Create(request.ContactPhoneNumber).Value;
        var volunteerPhoneNumber = PhoneNumber.Create(request.VolunteerPhoneNumber).Value;

        var pet = Pet.Create(
            request.Nickname,
            request.Color,
            address,
            place,
            weight,
            false,
            "fsdfsdf",
            contactPhoneNumber,
            volunteerPhoneNumber,
            true);

        var idResult = await _petsRepository.Add(pet.Value, ct);
        if (idResult.IsFailure)
            return idResult.Error;

        return idResult;
    }
}