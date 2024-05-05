using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreatePet;

public class CreatePetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreatePetHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ResultEvent>> Handle(CreatePetRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var address = Address.Create(request.City, request.Street, request.Building, request.Index).Value;
        var place = Place.Create(request.Place).Value;
        var weight = Weight.Create(request.Weight).Value;
        var contactPhoneNumber = PhoneNumber.Create(request.ContactPhoneNumber).Value;
        var volunteerPhoneNumber = PhoneNumber.Create(request.VolunteerPhoneNumber).Value;

        var pet = Pet.Create(
            request.Nickname,
            request.Description,
            request.BirthDate,
            request.Breed,
            request.Color,
            address,
            place,
            request.Castration,
            request.PeopleAttitude,
            request.AnimalAttitude,
            request.OnlyOneInFamily,
            request.Health,
            request.Height,
            weight,
            contactPhoneNumber,
            volunteerPhoneNumber,
            request.OnTreatment);

        if (pet.IsFailure)
            return pet.Error;

        volunteer.Value.PublishPet(pet.Value);

        await _volunteersRepository.Save(ct);

        return volunteer.Value.Id;
    }
}