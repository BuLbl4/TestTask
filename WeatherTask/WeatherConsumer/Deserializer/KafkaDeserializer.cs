using System.Text.Json;
using Confluent.Kafka;
using WeatherConsumer.Message;

namespace WeatherConsumer.Deserializer;

public class KafkaDeserializer : IDeserializer<WeatherMessage>
{
    public WeatherMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<WeatherMessage>(data)!;
    }
}