using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Pets.CreatePet;

public record CreatePetRequest(
    string Nickname,
    string Description,
    DateTimeOffset BirthDate,
    string Breed,
    string Color,
    string City,
    string Street,
    string Building,
    string Index,
    string Place,
    bool Castration,
    string PeopleAttitude,
    string AnimalAttitude,
    bool OnlyOneInFamily,
    string Health,
    int? Height,
    float Weight,
    string ContactPhoneNumber,
    string VolunteerPhoneNumber,
    bool OnTreatment);

public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{
    public CreatePetRequestValidator()
    {
        RuleFor(x => x.Nickname).NotEmpty().MaximumLength(100).WithError(Errors.General.InvalidLength());
        RuleFor(x => x.Description).MaximumLength(1500);
        RuleFor(x => x.BirthDate).GreaterThan(DateTimeOffset.UtcNow);
        RuleFor(x => x.Breed).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Color).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Castration).NotNull();
        RuleFor(x => x.PeopleAttitude).NotEmpty().MaximumLength(750);
        RuleFor(x => x.AnimalAttitude).NotEmpty().MaximumLength(750);
        RuleFor(x => x.OnlyOneInFamily).NotNull();
        RuleFor(x => x.Health).NotEmpty().MaximumLength(750);
        RuleFor(x => x.Height).NotNull();
        RuleFor(x => x.ContactPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.VolunteerPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.Weight).MustBeValueObject(Weight.Create);
        RuleFor(x => x.Place).MustBeValueObject(Place.Create);
        RuleFor(x => new { x.City, x.Street, x.Building, x.Index })
            .MustBeValueObject(x => Address.Create(x.City, x.Street, x.Building, x.Index));
        RuleFor(x => x.OnTreatment).NotNull();
    }
}