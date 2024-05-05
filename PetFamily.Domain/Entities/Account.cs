using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class Account : Entity
{
    public string Email { get; set; } = string.Empty;
    public PhoneNumber? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}