
namespace WeatherProducer.Abstraction;

public interface IKafkaProducerService<in T>
{
    Task ProduceAsync(T weatherDto, CancellationToken cancellationToken);
}