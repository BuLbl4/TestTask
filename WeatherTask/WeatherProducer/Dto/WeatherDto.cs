namespace WeatherProducer.Dto;

public class WeatherDto
{
    public double Temp { get; set; }
    
    public string Description { get; set; } = default!;
}