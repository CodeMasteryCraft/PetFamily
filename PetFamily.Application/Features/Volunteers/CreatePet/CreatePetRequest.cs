using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreatePet;

public record CreatePetRequest(
    Guid VolunteerId,
    string Nickname,
    string Color,
    string City,
    string Street,
    string Building,
    string Index,
    string Place,
    float Weight,
    bool OnlyOneInFamily,
    string Health,
    string ContactPhoneNumber,
    string VolunteerPhoneNumber,
    bool onTreatment);

public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{
    public CreatePetRequestValidator()
    {
        //TODO написать валидацию для всех свойств
        RuleFor(x => x.ContactPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.VolunteerPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.Weight).MustBeValueObject(Weight.Create);
        RuleFor(x => x.Place).MustBeValueObject(Place.Create);
        RuleFor(x => new { x.City, x.Street, x.Building, x.Index })
            .MustBeValueObject(x => Address.Create(x.City, x.Street, x.Building, x.Index));
    }
}