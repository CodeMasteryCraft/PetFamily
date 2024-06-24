using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Users;

public interface IUsersRepository
{
    Task<Result<User>> GetByEmail(string email, CancellationToken ct);
    Task<Result<User>> GetById(Guid id, CancellationToken ct);
    public Task Add(User user, CancellationToken ct);
}