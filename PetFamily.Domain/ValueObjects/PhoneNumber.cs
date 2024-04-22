using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record PhoneNumber
{
    private const int MAX_LENGTH_NUMBER = 18;

    private const string sngPhoneRegex = 
        @"^((8|\+374|\+994|\+995|\+375|\+7|\+380|\+38|\+996|\+998|\+993)
        [\- ]?)?\(?\d{3,5}\)?
        [\- ]?\d{1}[\- ]?\d{1}
        [\- ]?\d{1}[\- ]?\d{1}
        [\- ]?\d{1}(([\- ]?\d{1})?
        [\- ]?\d{1})?$";

    public string Number { get; }

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static Result<PhoneNumber, Error> Create(string input)
    {
        input = input.Trim();

        if (input.Length  is < 1 or > MAX_LENGTH_NUMBER)
            return Errors.General.InvalidLength("phone number");

        if (Regex.IsMatch(input, sngPhoneRegex) == false)
            return Errors.General.ValueIsInvalid("phone number");

        return new PhoneNumber(input);
    }
}