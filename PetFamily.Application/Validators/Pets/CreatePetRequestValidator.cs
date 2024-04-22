using Contracts.Requests;
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Validators.Pets;

public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{
    public CreatePetRequestValidator()
    {
        //Возможно валидацию всех типов можно было сделать в таком видео, но не смог реализовать проверку на null переменные bool в Pet,
        // а так же Pet должен иметь id и createdDate
        /*RuleFor(x => new
        {
            x.Nickname,
            x.Description,
            x.BirthDate,
            x.Breed,
            x.Color,
            x.PeopleAttitude,
            x.AnimalAttitude,
            x.Health,
            x.Height
        })
                .MustBeValueObject(x => Pet.Create(
                    x.Nickname, x.Description, x.BirthDate, x.Breed, x.Color, x.PeopleAttitude, x.Health, x.Height));*/

        RuleFor(x => x.Nickname).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(1500).WithError(Errors.General.InvalidLength());
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