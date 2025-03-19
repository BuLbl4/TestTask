using Confluent.Kafka;
using Microsoft.Extensions.Options;
using WeatherConsumer.Deserializer;
using WeatherConsumer.Message;
using WeatherConsumer.Options;
using WeatherProducer.Options;

namespace WeatherConsumer.Consumers;

public class Consumer : BackgroundService
{
    private readonly IConsumer<string, WeatherMessage> _consumer;
    private readonly IOptions<GrpcClientOptions> _grpcClientOptions;
    private readonly ILogger<Consumer> _logger;

    public Consumer(IOptions<KafkaOptions> kafkaOptions,
        IOptions<GrpcClientOptions> grpcClientOptions,
        ILogger<Consumer> logger)
    {
        _grpcClientOptions = grpcClientOptions;
        _logger = logger;
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaOptions.Value.BootstrapServers,
            GroupId = kafkaOptions.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var topic = kafkaOptions.Value.Topic;
        _consumer = new ConsumerBuilder<string, WeatherMessage>(config)
            .SetValueDeserializer(new KafkaDeserializer())
            .Build();

        _consumer.Subscribe(topic);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var grpcClient = new WeatherGrpcClient(_grpcClientOptions.Value.Url);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                var weatherData = consumeResult.Message.Value;
                _logger.LogInformation("Отправляем через gRPC");
                await grpcClient.SetWeatherAsync(weatherData.Temp, weatherData.Description);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        finally
        {
            _consumer.Close();
            _consumer.Dispose();
            grpcClient.Dispose();
        }
    }
}