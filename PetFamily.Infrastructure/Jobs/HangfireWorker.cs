using Hangfire;

namespace PetFamily.Infrastructure.Jobs;

public static class HangfireWorker
{
    public static void StartRecurringJobs()
    {
        RecurringJob.AddOrUpdate<IImageCleanupJob>(
            "image-cleaner",
            job => job.ProccessAsync(),
            Cron.Daily);
    }
}