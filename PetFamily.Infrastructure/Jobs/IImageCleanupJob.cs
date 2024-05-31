namespace PetFamily.Infrastructure.Jobs;

public interface IImageCleanupJob
{
    Task ProccessAsync();
}