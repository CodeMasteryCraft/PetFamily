using CSharpFunctionalExtensions;
using PetFamily.Application.DataAccess;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreatePet;

public class CreatePetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _dbContext;

    public CreatePetHandler(IVolunteersRepository volunteersRepository, IUnitOfWork dbContext)
    {
        _volunteersRepository = volunteersRepository;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Handle(CreatePetRequest request, CancellationToken ct)
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
            request.OnTreatment,
            request.Vaccinations);

        if (pet.IsFailure)
            return pet.Error;

        volunteer.Value.PublishPet(pet.Value);

        await _dbContext.SaveChangesAsync(ct);
        return volunteer.Value.Id;
    }
}