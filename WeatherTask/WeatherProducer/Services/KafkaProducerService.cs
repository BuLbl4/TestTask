using Confluent.Kafka;
using Microsoft.Extensions.Options;
using WeatherProducer.Abstraction;
using WeatherProducer.Options;
using WeatherProducer.Serializer;

namespace WeatherProducer.Services;

public class KafkaProducerService<T> : IKafkaProducerService<T>, IDisposable
{
    private readonly ILogger<KafkaProducerService<T>> _logger;
    private readonly IProducer<string, T> _producer;
    private readonly string _topic;
    
    public KafkaProducerService(IOptions<KafkaOptions> kafkaOptions, ILogger<KafkaProducerService<T>> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = kafkaOptions.Value.BootstrapServers
        };

        _producer = new ProducerBuilder<string, T>(config)
            .SetValueSerializer(new KafkaSerializer<T>())
            .Build();

        _topic = kafkaOptions.Value.Topic;
    }
    
    public async Task ProduceAsync(T weatherDto, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(_topic, new Message<string, T>
        {
            Key = "uniq",
            Value = weatherDto
        }, cancellationToken);
        _logger.LogInformation("передали данные");
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}
