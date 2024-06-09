using PetFamily.Application.Messages;

namespace PetFamily.Application.Providers;

public interface IMailProvider
{
    Task SendMessage(EmailNotification emailNotification);
}