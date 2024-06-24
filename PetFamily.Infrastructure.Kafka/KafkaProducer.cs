using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Domain.Common;

namespace PetFamily.Infrastructure.Kafka;

public class KafkaProducer<T>
{
    private readonly ILogger<T> _logger;
    private readonly IProducer<Null, T> _producer;
    private readonly KafkaOptions _kafkaOptions;

    public KafkaProducer(ILogger<T> logger, IOptions<KafkaOptions> kafkaOptions)
    {
        _logger = logger;
        _kafkaOptions = kafkaOptions.Value;
        _producer = CreateProducer();
    }

    public async Task<Result> Publish(string topic, T message)
    {
        var kafkaMessage = new Message<Null, T>
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

    private IProducer<Null, T> CreateProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _kafkaOptions.Host,
            AllowAutoCreateTopics = true,
            ClientId = _kafkaOptions.ClientId,
            MessageSendMaxRetries = 3
        };

        return new ProducerBuilder<Null, T>(config)
            .SetValueSerializer(new KafkaSerializer<T>())
            .Build();
    }
}