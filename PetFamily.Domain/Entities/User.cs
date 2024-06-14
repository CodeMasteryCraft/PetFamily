using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class User : Entity
{
    private User()
    {
    }

    private User(Email email, string passwordHash, Role role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Role Role { get; private set; }

    public static Result<User> CreateVolunteer(Email email, string passwordHash)
    {
        return new User(email, passwordHash, Role.Volunteer);
    }

    public static Result<User> CreateAdmin(Email email, string passwordHash)
    {
        return new User(email, passwordHash, Role.Admin);
    }

    public static Result<User> CreateRegularUser(Email email, string passwordHash)
    {
        return new User(email, passwordHash, Role.RegularUser);
    }
}