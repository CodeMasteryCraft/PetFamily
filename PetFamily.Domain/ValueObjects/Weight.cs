using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using ValueObject = PetFamily.Domain.Common.ValueObject;

namespace PetFamily.Domain.ValueObjects;

public class Weight : ValueObject
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

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Kilograms;
    }
}