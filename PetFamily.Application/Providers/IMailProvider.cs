using PetFamily.Application.Messages;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Providers;

public interface IMailProvider
{
    Task SendMessage(EmailNotification emailNotification);
}