using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record Weight
{
    public float Kilograms { get; set; }

    private Weight(float kilograms)
    {
        Kilograms = kilograms;
    }

    public static Result<Weight, IReadOnlyList<Error>> Create(float kilograms)
    {
        return ResultBuilder.Create()
            .AddErrorCondition(() => kilograms <= 0, () => Errors.General.ValueIsInvalid("weight"))
            .Build(() => new Weight(kilograms));
    }
}