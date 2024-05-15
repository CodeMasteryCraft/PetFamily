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