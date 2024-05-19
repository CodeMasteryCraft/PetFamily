using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Providers
{
    public interface IJwtProvider
    {
        Result<string, Error> Create(string password, User user);
    }
}