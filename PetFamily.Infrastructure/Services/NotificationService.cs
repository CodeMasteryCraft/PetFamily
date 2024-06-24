using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Users;
using PetFamily.Application.Messages;
using PetFamily.Application.Models;
using PetFamily.Application.Providers;
using PetFamily.Application.Services;
using PetFamily.Domain.Common;

namespace PetFamily.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IUsersRepository _usersRepository;
    private readonly IMailProvider _mailProvider;

    public NotificationService(
        ILogger<NotificationService> logger,
        IUsersRepository usersRepository,
        IMailProvider mailProvider)
    {
        _logger = logger;
        _usersRepository = usersRepository;
        _mailProvider = mailProvider;
    }

    public async Task<Result> Notify(Notification notification, CancellationToken ct)
    {
        var user = await _usersRepository.GetById(notification.UserId, ct);
        if (user.IsFailure)
            return user.Error;

        var emailNotification = new EmailNotification(notification.Message, user.Value.Email);
        await _mailProvider.SendMessage(emailNotification);

        if (user.Value.TelegramId is not null)
        {
            // отправить сообщение по телеграму
        }

        return Result.Success();
    }
}