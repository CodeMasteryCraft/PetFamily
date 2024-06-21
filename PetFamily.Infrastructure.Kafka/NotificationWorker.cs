using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PetFamily.Infrastructure.Kafka;

public class NotificationWorker : BackgroundService
{
    private readonly ILogger<NotificationWorker> _logger;

    public NotificationWorker(ILogger<NotificationWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true,
            EnableAutoCommit = false
        };

        try
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("test-topic");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var kafkaMessage = consumer.Consume(stoppingToken);

                    if (kafkaMessage is null)
                    {
                        _logger.LogInformation("Message is null");
                        continue;
                    }

                    // обработка (отправка email)

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
}