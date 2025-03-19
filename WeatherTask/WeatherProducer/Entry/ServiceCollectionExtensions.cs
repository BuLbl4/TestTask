using WeatherProducer.Abstraction;
using WeatherProducer.Dto;
using WeatherProducer.Options;
using WeatherProducer.Services;

namespace WeatherProducer.Entry;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWeatherService(configuration);
        services.AddKafkaProducer<WeatherDto>(configuration);
        services.AddHostedService<WeatherBackgroundService>();
    }

    private static void AddKafkaProducer<T>(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));
        services.AddSingleton<IKafkaProducerService<T>, KafkaProducerService<T>>();
    }

    private static void AddWeatherService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WeatherOptions>(configuration.GetSection("WeatherApi"));
        services.AddScoped<IWeatherService, WeatherService>();
    }
}