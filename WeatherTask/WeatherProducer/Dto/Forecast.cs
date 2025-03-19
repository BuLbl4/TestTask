namespace WeatherProducer.Dto;

public class Forecast
{
    public long Dt { get; set; }
    public Main Main { get; set; } = default!;
    
    public List<Weather> Weather { get; set; } = default!;
}