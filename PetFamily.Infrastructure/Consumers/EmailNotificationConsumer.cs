using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.MessageBuses;

namespace PetFamily.Infrastructure.Consumers;

public class EmailNotificationConsumer(
    EmailMessageChannel channel,
    IServiceScopeFactory scopeFactory,
    ILogger<EmailNotificationConsumer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Email notification consumer started work");

            var emailNotification = await channel.Reader.ReadAsync(stoppingToken);

            await using var scope = scopeFactory.CreateAsyncScope();
            var mailProvider = scope.ServiceProvider.GetRequiredService<IMailProvider>();

            await mailProvider.SendMessage(emailNotification);
            logger.LogInformation("Email notification has been sended");
        }

        logger.LogInformation("Email notification consumer stoped work");
    }
}