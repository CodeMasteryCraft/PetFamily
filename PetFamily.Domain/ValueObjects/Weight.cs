using Contracts.ConstantValue;
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
        if (kilograms is <= 0 or PetConstant.MAX_WEIGHT_LIMIT)
            return Errors.General.ValueIsInvalid("weight");

        return new Weight(kilograms);
    }
}