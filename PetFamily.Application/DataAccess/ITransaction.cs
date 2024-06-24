namespace PetFamily.Application.DataAccess;

public interface ITransaction
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}