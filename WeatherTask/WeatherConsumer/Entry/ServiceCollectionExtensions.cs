using WeatherConsumer.Consumers;
using WeatherConsumer.Deserializer;
using WeatherConsumer.Options;
using WeatherProducer.Options;

namespace WeatherConsumer.Entry;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));
        services.Configure<GrpcClientOptions>(configuration.GetSection("GrpcClient"));
        services.AddSingleton<KafkaDeserializer>();
        services.AddHostedService<Consumer>();
    }
}