using PetFamily.Application.DataAccess;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Providers;

public class Transaction : ITransaction
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public Transaction(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}