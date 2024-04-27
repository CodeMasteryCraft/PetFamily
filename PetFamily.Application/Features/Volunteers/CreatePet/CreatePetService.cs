using CSharpFunctionalExtensions;
using PetFamily.Application.Features.Pets;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreatePet;

public class CreatePetService
{
    private readonly IPetsRepository _petsRepository;
    private readonly IVolunteerRepository _volunteerRepository;

    public CreatePetService(IPetsRepository petsRepository, IVolunteerRepository volunteerRepository)
    {
        _petsRepository = petsRepository;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreatePetRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteerRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

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

        if (pet.IsFailure)
            return pet.Error;

        volunteer.Value.PublishPet(pet.Value);

        return await _volunteerRepository.Save(volunteer.Value, ct);
    }
}