namespace WeatherConsumer.Message;

public class WeatherMessage
{
    public double Temp { get; set; }
    
    public string Description { get; set; } = default!;
}