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

        if (city.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(city));

        if (street.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.MEDIUM_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(street));

        if (building.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(building));

        if (index.Length != Constraints.INDEX_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(index));

        return new Address(city, street, building, index);
    }
}