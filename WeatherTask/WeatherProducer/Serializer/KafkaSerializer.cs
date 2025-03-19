using System.Text.Json;
using Confluent.Kafka;

namespace WeatherProducer.Serializer;

public class KafkaSerializer<T> : ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }
}