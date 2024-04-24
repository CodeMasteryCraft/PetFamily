using Contracts.Requests;
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;
using System.Security.Cryptography.X509Certificates;

namespace PetFamily.Application.Validators.Pets;

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