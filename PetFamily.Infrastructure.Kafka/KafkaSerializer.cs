using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace PetFamily.Infrastructure.Kafka;

public class KafkaSerializer<T> : ISerializer<T>, IDeserializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        var dataString = JsonSerializer.Serialize(data);

        return Encoding.UTF8.GetBytes(dataString);
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var dataString = Encoding.UTF8.GetString(data);

        var model = JsonSerializer.Deserialize<T>(dataString);

        if (model is null)
        {
            throw new ArgumentException("Data in kafka topic is null");
        }

        return model;
    }
}