using FluentValidation;

namespace PetFamily.Application.Features.Pets.GetPets;

public record GetPetsRequest(
    string? Nickname,
    string? Breed,
    string? Color,
    int Size = 10,
    int Page = 1);

public class GetPetsValidator : AbstractValidator<GetPetsRequest>
{
    public GetPetsValidator()
    {
        RuleFor(x => x.Page).NotNull().GreaterThan(0);
        RuleFor(x => x.Size).NotNull().GreaterThan(0);
    }
}