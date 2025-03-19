using WeatherDataService.Domain.Common;

namespace WeatherDataService.Domain.Entities;

public class Weather : BaseEntity
{
    public string Description { get; set; } = default!;

    public double Temp { get; set; }
    
    public DateOnly Date { get; set; } 
    
    public TimeOnly Time { get; set; }

}