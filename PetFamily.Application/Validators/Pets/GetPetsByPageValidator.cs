using Contracts.Pets.Requests;
using FluentValidation;

namespace PetFamily.Application.Validators.Pets;

public class GetPetsByPageValidator : AbstractValidator<GetPetsByPageRequest>
{
    public GetPetsByPageValidator()
    {
        RuleFor(x => x.Page).NotNull().GreaterThan(0);
        RuleFor(x => x.Size).NotNull().GreaterThan(0);
    }
}