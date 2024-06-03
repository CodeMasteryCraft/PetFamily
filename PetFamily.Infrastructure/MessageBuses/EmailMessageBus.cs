using Microsoft.Extensions.Logging;
using PetFamily.Application.MessageBus;
using PetFamily.Application.Messages;

namespace PetFamily.Infrastructure.MessageBuses;

public class EmailMessageBus(
    EmailMessageChannel messageChannel,
    ILogger<EmailMessageChannel> logger) : IMessageBus
{
    public async Task PublishAsync(EmailNotification emailNotification, CancellationToken ct)
    {
        await messageChannel.Writer.WriteAsync(emailNotification, ct);
        logger.LogInformation("Email message successfully delivered");
    }
}