using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Models;
using PetFamily.Infrastructure.Kafka;

namespace PetFamily.Infrastructure.TelegramBot;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructureTelegram(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.Telegram));
        services.AddHostedService<TelegramWorker>();

        return services;
    }
}