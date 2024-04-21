using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public partial record PhoneNumber
{
    [GeneratedRegex("^(\\+7|8)\\d{10}$")]
    private static partial Regex PhoneRegex();

    public string Number { get; }

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static Result<PhoneNumber, IReadOnlyList<Error>> Create(string input)
    {
        input = string.Concat(input.Where(chr => chr >= '0' && chr <= '9').Take(13));

        return ResultBuilder.Create()
            .AddErrorCondition(() => PhoneRegex().IsMatch(input) == false, () => Errors.General.ValueIsInvalid("phone number"))
            .Build(() => new PhoneNumber($"+{input}"));
    }
}