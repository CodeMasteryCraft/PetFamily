using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreatePet;

public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{
    public CreatePetRequestValidator()
    {
        //добавить метод WithError
        RuleFor(x => x.ContactPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.VolunteerPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.Weight).MustBeValueObject(Weight.Create);
        RuleFor(x => x.Place).MustBeValueObject(Place.Create);
        RuleFor(x => new { x.City, x.Street, x.Building, x.Index })
            .MustBeValueObject(x => Address.Create(x.City, x.Street, x.Building, x.Index));

        RuleFor(x => x).NotNull();
        RuleFor(x => x).NotEmpty();

        RuleFor(x => x.Nickname).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.Description).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.MAXIMUM_TITLE_LENGTH);

        RuleFor(x => x.BirthDate).LessThan(DateTimeOffset.UtcNow);
        RuleFor(x => x.BirthDate.Year).GreaterThan(Constraints.YEAR1900);

        RuleFor(x => x.Breed).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.Color).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.City).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.MEDIUM_TITLE_LENGTH);
        RuleFor(x => x.Street).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);
        RuleFor(x => x.Building).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);
        RuleFor(x => x.Index.Length).Equal(Constraints.INDEX_TITLE_LENGTH);

        RuleFor(x => x.Place).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.PeopleAttitude).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.AnimalAttitude).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.Health).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.MAXIMUM_TITLE_LENGTH);

        RuleFor(x => x.Height).GreaterThan(0);

        RuleFor(x => x.Weight).GreaterThan(0);

        RuleFor(x => x.ContactPhoneNumber).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.VolunteerPhoneNumber).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.CreatedDate).LessThan(DateTimeOffset.UtcNow);
        RuleFor(x => x.CreatedDate.Year).GreaterThan(Constraints.YEAR1900);
    }
}