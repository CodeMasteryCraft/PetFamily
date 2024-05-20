using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Providers
{
    public interface IJwtProvider
    {
        Result<string, Error> Generate(User user);
    }
}