using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreatePet;

public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{
    public CreatePetRequestValidator()
    {
        RuleFor(x => x.ContactPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.VolunteerPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.Weight).MustBeValueObject(Weight.Create);
        RuleFor(x => x.Place).MustBeValueObject(Place.Create);
        RuleFor(x => new { x.City, x.Street, x.Building, x.Index })
            .MustBeValueObject(x => Address.Create(x.City, x.Street, x.Building, x.Index));

        RuleFor(x => x.Nickname)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
        
        RuleFor(x => x.Description)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
        
        RuleFor(x => x.BirthDate)
            .LessThanWithError(DateTimeOffset.UtcNow);
        
        RuleFor(x => x.Breed)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
        
        RuleFor(x => x.Color)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
        
        RuleFor(x => x.Place)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
        
        RuleFor(x => x.PeopleAttitude)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
        
        RuleFor(x => x.AnimalAttitude)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
        
        RuleFor(x => x.Health)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
        
        RuleFor(x => x.Height).GreaterThanWithError(0);
    }
}