using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Users;

public interface IUsersRepository
{
    Task<Result<User, Error>> GetByEmail(string email, CancellationToken ct);
    public Task Add(User user, CancellationToken ct);
}