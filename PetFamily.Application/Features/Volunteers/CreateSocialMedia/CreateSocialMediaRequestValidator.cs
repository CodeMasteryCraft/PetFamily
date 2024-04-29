using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateSocialMedia;

public class CreateSocialMediaRequestValidator : AbstractValidator<CreateSocialMediaRequest>
{
    public CreateSocialMediaRequestValidator()
    {
        // добавить метод WithError
        RuleFor(x => x.Social).MustBeValueObject(Social.Create);
        
        RuleFor(x => x).NotNull();
        RuleFor(x => x).NotEmpty();
        
        RuleFor(x => x.Link).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.MAXIMUM_TITLE_LENGTH);
    }
}