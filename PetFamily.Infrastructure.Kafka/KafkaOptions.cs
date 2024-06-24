namespace PetFamily.Infrastructure.Kafka;

public class KafkaOptions
{
    public const string Kafka = nameof(Kafka);

    public string Host { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string NotificationsTopic { get; set; } = string.Empty;
    public string NotificationsGroupId { get; set; } = string.Empty;
    public int NotificationsTopicPartitions { get; set; } = 1;
}