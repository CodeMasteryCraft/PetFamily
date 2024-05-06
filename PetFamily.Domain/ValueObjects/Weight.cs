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

    public static Result<Weight, Error> Create(float kilograms)
    {
        if (kilograms <= 0)
            return Errors.General.ValueIsInvalid(nameof(Weight));

        return new Weight(kilograms);
    }
}