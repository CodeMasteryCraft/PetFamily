using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using ValueObject = PetFamily.Domain.Common.ValueObject;

namespace PetFamily.Domain.ValueObjects;

public class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }

    private FullName(string firstName, string lastName, string? patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string firstName, string lastName, string? patronymic)
    {
        firstName = firstName.Trim();
        lastName = lastName.Trim();
        patronymic = patronymic?.Trim();

        if (firstName.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (lastName.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        if (patronymic?.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        return new FullName(firstName, lastName, patronymic);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}