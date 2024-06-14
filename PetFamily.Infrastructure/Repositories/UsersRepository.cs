using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Users;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public UsersRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user, CancellationToken ct)
    {
        await _dbContext.AddAsync(user, ct);
    }

    public async Task<Result<User, Error>> GetByEmail(string email, CancellationToken ct)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken: ct);

        if (user is null)
            return Errors.General.NotFound();

        return user;
    }
}