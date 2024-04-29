using FluentValidation;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Pets.CreateVaccination;

public class CreateVaccinationRequestValidator : AbstractValidator<CreateVaccinationRequest>
{
    public CreateVaccinationRequestValidator()
    {
        RuleForEach(x => x.Vaccinations).ChildRules(order => 
        {
            order.RuleFor(x => x.Name).
                MinimumLength(Constraints.MINIMUM_TITLE_LENGTH).
                MaximumLength(Constraints.SHORT_TITLE_LENGTH);
        });

        RuleForEach(x => x.Vaccinations).ChildRules(order => 
        {
            order.RuleFor(x => x.Applied).LessThan(DateTimeOffset.UtcNow);
        });
    }
}