using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Common;

namespace PetFamily.Infrastructure.Kafka;

public class KafkaMessageProducer
{
    private readonly ILogger<KafkaMessageProducer> _logger;
    private readonly IProducer<Null, string> _producer;

    public KafkaMessageProducer(ILogger<KafkaMessageProducer> logger)
    {
        _logger = logger;
        _producer = CreateProducer();
    }

    public async Task<Result> Publish(string topic, string message)
    {
        var kafkaMessage = new Message<Null, string>
        {
            Value = message
        };

        var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);

        if (deliveryResult.Status == PersistenceStatus.NotPersisted)
        {
            _logger.LogError("Message not persisted: {message}", kafkaMessage.Value);
            return Errors.Kafka.PersistFail();
        }

        _logger.LogInformation("Message persisted: {message}", kafkaMessage.Value);
        return Result.Success();
    }

    private IProducer<Null, string> CreateProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            AllowAutoCreateTopics = true,
            ClientId = "PetFamily",
            MessageSendMaxRetries = 3
        };

        return new ProducerBuilder<Null, string>(config).Build();
    }
}