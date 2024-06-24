using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Models;

namespace PetFamily.Infrastructure.Kafka;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructureKafka(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<NotificationConsumer>();
        services.AddSingleton<KafkaProducer<Notification>>();
        services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Kafka));
        services.AddSingleton<KafkaSerializer<Notification>>();

        return services;
    }
}