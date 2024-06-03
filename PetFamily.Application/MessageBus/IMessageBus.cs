using PetFamily.Application.Messages;

namespace PetFamily.Application.MessageBus;

public interface IMessageBus
{
    Task PublishAsync(EmailNotification emailNotification, CancellationToken ct);
}