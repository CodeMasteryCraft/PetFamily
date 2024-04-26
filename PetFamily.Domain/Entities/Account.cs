using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class Account
{
    private Account(Guid id, string email, PhoneNumber userPhoneNumber, string passwordHash)
    {
        Id = id;
        Email = email;
        UserPhoneNumber = userPhoneNumber;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public PhoneNumber UserPhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }

    public static Account Create(Guid id, string email, PhoneNumber userphoneNumber, string passwordHash)
    {


        return new Account(id,email, userphoneNumber, passwordHash);
    }
}