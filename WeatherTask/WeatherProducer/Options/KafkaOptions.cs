namespace WeatherProducer.Options;

public class KafkaOptions
{
    public string BootstrapServers { get; set; } = default!;

    public string Topic { get; set; } = default!;

    public string GroupId { get; set; } = default!;
}