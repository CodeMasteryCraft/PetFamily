using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Application.Models;
using PetFamily.Application.Services;

namespace PetFamily.Infrastructure.Kafka;

public class NotificationConsumer : BackgroundService
{
    private readonly ILogger<NotificationConsumer> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly KafkaOptions _kafkaOptions;

    public NotificationConsumer(
        ILogger<NotificationConsumer> logger,
        IOptions<KafkaOptions> kafkaOptions,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _kafkaOptions = kafkaOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        try
        {
            using (var consumer = CreateConsumer())
            {
                consumer.Subscribe(_kafkaOptions.NotificationsTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var kafkaMessage = consumer.Consume(stoppingToken);

                    if (kafkaMessage is null)
                    {
                        _logger.LogInformation("Message is null");
                        continue;
                    }

                    var scope = _scopeFactory.CreateScope();

                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    await notificationService.Notify(kafkaMessage.Message.Value, stoppingToken);

                    _logger.LogInformation("Message consumed: {message}", kafkaMessage.Message.Value);

                    consumer.Commit(kafkaMessage);
                }

                consumer.Close();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while consuming kafka");
        }

        await Task.CompletedTask;
    }

    public IConsumer<Ignore, Notification> CreateConsumer()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaOptions.Host,
            GroupId = _kafkaOptions.NotificationsGroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true,
            EnableAutoCommit = false
        };

        return new ConsumerBuilder<Ignore, Notification>(config)
            .SetValueDeserializer(new KafkaSerializer<Notification>())
            .Build();
    }
}