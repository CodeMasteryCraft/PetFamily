using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmptyWithError();
        RuleFor(x => x.LastName).NotEmptyWithError();
        RuleForEach(x => x.SocialMedias).ChildRules(s =>
        {
            s.RuleFor(x => x.Link)
                .NotEmptyWithError()
                .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

            s.RuleFor(x => x.Social).MustBeValueObject(Social.Create);
        }).When(x => x.SocialMedias != null);

        RuleFor(v => new { v.FirstName, v.LastName, v.Patronymic })
            .MustBeValueObject(v => FullName.Create(v.FirstName, v.LastName, v.Patronymic));

        RuleFor(v => v.Description)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

        RuleFor(v => v.YearsExperience)
            .GreaterThanWithError(0);

        RuleFor(v => v.NumberOfPetsFoundHome)
            .GreaterThanWithError(0)
            .When(x => x != null);

        RuleFor(v => v.DonationInfo!)
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH)
            .When(x => x != null);
    }
}