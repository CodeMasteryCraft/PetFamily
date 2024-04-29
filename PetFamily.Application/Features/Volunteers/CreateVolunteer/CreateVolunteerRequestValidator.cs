using System.ComponentModel.DataAnnotations;
using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        // написать метод WithError
        RuleForEach(x => x.SocialMedias).ChildRules(order => 
        {
            order.RuleFor(x => x.Social).
                MinimumLength(Constraints.MINIMUM_TITLE_LENGTH).
                MaximumLength(Constraints.SHORT_TITLE_LENGTH);
        });
        
        RuleForEach(x => x.SocialMedias).ChildRules(order => 
        {
            order.RuleFor(x => x.Link).
                MinimumLength(Constraints.MINIMUM_TITLE_LENGTH).
                MaximumLength(Constraints.MAXIMUM_TITLE_LENGTH);
        });
        
        RuleFor(v => v).NotNull();
        RuleFor(v => v).NotEmpty();
        
        RuleFor(v => v.Name).MaximumLength(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(v => v.Description).MinimumLength(Constraints.MINIMUM_TITLE_LENGTH)
            .MaximumLength(Constraints.MAXIMUM_TITLE_LENGTH);

        RuleFor(v => v.YearsExperience).GreaterThan(0);
        
        RuleFor(v => v.NumberOfPetsFoundHome).GreaterThan(0);

        RuleFor(v => v.DonationInfo).MaximumLength(Constraints.MAXIMUM_TITLE_LENGTH);
    }
}