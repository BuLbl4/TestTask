using WeatherProducer.Abstraction;
using WeatherProducer.Dto;

namespace WeatherProducer.Services;

public class WeatherBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IKafkaProducerService<WeatherDto>> _logger;
    private readonly IKafkaProducerService<WeatherDto> _kafkaService;

    public WeatherBackgroundService(
        IKafkaProducerService<WeatherDto> kafkaService,
        IServiceProvider serviceProvider,
        ILogger<IKafkaProducerService<WeatherDto>> logger)
    {
        _kafkaService = kafkaService;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();
                
                var weatherData = await weatherService.FetchWeather();
                await _kafkaService.ProduceAsync(weatherData, stoppingToken);
                
                await Task.Delay(1000 * 60, stoppingToken);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}