using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record PhoneNumber
{
    public string Number { get; }

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static Result<PhoneNumber, Error> Create(string input)
    {
        input = input.Trim();

        if (input.IsEmpty() || input.Length < Constraints.MINIMUM_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(PhoneNumber));

        if (Regex.IsMatch(input, Constraints.RUSSIAN_PHONE_REGEX) == false)
            return Errors.General.ValueIsInvalid(nameof(PhoneNumber));

        return new PhoneNumber(input);
    }
}