using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record Address
{
    private Address(string city, string street, string building, string index)
    {
        City = city;
        Street = street;
        Building = building;
        Index = index;
    }

    public string City { get; }
    public string Street { get; }
    public string Building { get; }
    public string Index { get; }

    public static Result<Address, IReadOnlyList<Error>> Create(string city, string street, string building, string index)
    {
        city = city.Trim();
        street = street.Trim();
        building = building.Trim();
        index = index.Trim();

        return ResultBuilder.Create()
            .AddErrorCondition(() => city.Length is < 1 or > 100, () => Errors.General.InvalidLength("city"))
            .AddErrorCondition(() => street.Length is < 1 or > 100, () => Errors.General.InvalidLength("street"))
            .AddErrorCondition(() => building.Length is < 1 or > 100, () => Errors.General.InvalidLength("building"))
            .AddErrorCondition(() => index.Length != 6, () => Errors.General.InvalidLength("index"))
            .Build(() => new Address(city, street, building, index));
    }
}