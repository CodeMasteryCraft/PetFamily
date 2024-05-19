using PetFamily.Domain.Entities;

namespace PetFamily.Application.Providers;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}