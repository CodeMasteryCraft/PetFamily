using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record Place
{
    public static readonly Place InHospital = new(nameof(InHospital));
    public static readonly Place AtHome = new(nameof(AtHome));

    private static readonly Place[] _all = [InHospital, AtHome];

    public string Value { get; }

    private Place(string value)
    {
        Value = value;
    }

    public static Result<Place, IReadOnlyList<Error>> Create(string input)
    {
        var place = input.Trim();

        return ResultBuilder.Create()
            .AddErrorCondition(
                () => _all.Any(p => p.Value.Equals(place, StringComparison.InvariantCultureIgnoreCase)) == false,
                () => Errors.General.ValueIsRequried("input"))
            .Build(() => new Place(place));


        /*if (input.IsEmpty())
            return Errors.General.ValueIsRequried("input");

        var place = input.Trim().ToUpper();

        if (_all.Any(p => p.Value == place) == false)
        {
            return Errors.General.ValueIsInvalid("place");
        }

        return new Place(place);*/
    }
}