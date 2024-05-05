using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleForEach(x => x.SocialMedias).ChildRules(s =>
        {
            s.RuleFor(x => x.Link)
                .NotEmptyWithError()
                .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

            s.RuleFor(x => x.Social).MustBeValueObject(Social.Create);
        }).When(x => x.SocialMedias != null);

        RuleFor(v => v.Name)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(v => v.Description)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

        RuleFor(v => v.YearsExperience).GreaterThanOrEqualToWithError(0);

        RuleFor(v => v.NumberOfPetsFoundHome)
            .GreaterThanOrEqualToWithError(0)
            .When(x => x != null);

        RuleFor(v => v.DonationInfo!)
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH)
            .When(x => x != null);
    }
}