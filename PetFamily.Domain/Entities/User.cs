using System.Text.RegularExpressions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class User : Entity
{
    private User()
    {
    }

    public User(string email, string passwordHash, Role role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Role Role { get; private set; }
}

public class Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string input)
    {
        input = input.Trim();

        if (input.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength("email");

        if (Regex.IsMatch(input, "^(.+)@(.+)$") == false)
            return Errors.General.ValueIsInvalid("email");

        return new Email(input);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}