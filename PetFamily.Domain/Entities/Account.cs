using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
}