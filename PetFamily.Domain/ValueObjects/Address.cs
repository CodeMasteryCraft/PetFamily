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

    public static Result<Address, Error> Create(string city, string street, string building, string index)
    {
        city = city.Trim();
        street = street.Trim();
        building = building.Trim();
        index = index.Trim();

        if (city.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.MEDIUM_TITLE_LENGTH)
            return Errors.General.InvalidLength("city");

        if (street.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength("street");

        if (building.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength("building");

        if (index.Length != 6)
            return Errors.General.InvalidLength("index");

        return new Address(city, street, building, index);
    }
}