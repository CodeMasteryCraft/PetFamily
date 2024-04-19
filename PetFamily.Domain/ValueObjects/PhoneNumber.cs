using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record PhoneNumber
{
    private const string russionPhoneRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

    public string Number { get; }

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static Result<PhoneNumber, Error> Create(string input)
    {
        if (input.IsEmpty())
            return Errors.General.ValueIsRequired();

        if (Regex.IsMatch(input, russionPhoneRegex) == false)
            return Errors.General.ValueIsInvalid("phone number");

        return new PhoneNumber(input);
    }
}