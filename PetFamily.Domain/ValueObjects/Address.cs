using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.ValueObjects;

public record Address
{
    public const int MAX_LENGTH_ADDRESS = 150;

    public const string russianPostcodeREgax = @"^([1-6]{1}[0-9]{5})$";

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

        if (city.Length is < 1 or > MAX_LENGTH_ADDRESS)
            return Errors.General.InvalidLength("city");

        if (street.Length is < 1 or > MAX_LENGTH_ADDRESS)
            return Errors.General.InvalidLength("street");

        if (building.Length is < 1 or > MAX_LENGTH_ADDRESS)
            return Errors.General.InvalidLength("building");

        if (Regex.IsMatch(index, russianPostcodeREgax) == false)
            return Errors.General.ValueIsInvalid("index");

        return new Address(city, street, building, index);
    }
}