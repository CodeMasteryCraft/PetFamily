using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Application.DataAccess;
using PetFamily.Application.Features.Users;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PetFamily.Infrastructure.TelegramBot;

public class TelegramWorker : BackgroundService
{
    private readonly ILogger<TelegramWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TelegramOptions _telegramOptions;
    private readonly Dictionary<string, string> _messages = new();

    public TelegramWorker(
        IOptions<TelegramOptions> telegramOptions,
        ILogger<TelegramWorker> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _telegramOptions = telegramOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = []
        };

        var botClient = new TelegramBotClient(_telegramOptions.Token);


        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await botClient.ReceiveAsync(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandleErrorsAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: stoppingToken
                );
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while receive telegram message");
            }
        }
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        var inlineKeyboard = new InlineKeyboardMarkup(
            [
                InlineKeyboardButton.WithCallbackData("Войти", "login")
            ]
        );

        switch (update.CallbackQuery?.Message?.Text)
        {
            case "login":
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Введите свою почту",
                    cancellationToken: cancellationToken);
                break;
        }

        switch (messageText)
        {
            case "/start":
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Добро пожаловать!",
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
                break;

            default:
                if (!_messages.TryAdd("email", messageText))
                {
                    _messages["password"] = messageText;

                    var scope = _scopeFactory.CreateScope();

                    var usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
                    var transaction = scope.ServiceProvider.GetRequiredService<ITransaction>();

                    var user = await usersRepository.GetByEmail(_messages["email"], cancellationToken);

                    if (user.IsFailure)
                        break;

                    var isVerified = BCrypt.Net.BCrypt.Verify(_messages["password"], user.Value.PasswordHash);
                    if (isVerified == false)
                        break;

                    user.Value.AddTelegram(chatId);
                    await transaction.SaveChangesAsync(cancellationToken);
                }

                break;
        }

        _logger.LogInformation("Received a '{messageText}' message in chat {chatId}.", messageText, chatId);
    }

    private Task HandleErrorsAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation(ErrorMessage);
        return Task.CompletedTask;
    }
}