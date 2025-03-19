using WeatherDataService.Domain.Entities;

namespace WeatherDataService.Web.GraphQLQuery;

public class Query
{
    private readonly Services.WeatherService _weatherService;

    public Query(Services.WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [GraphQLName("GetLastWeatherRecords")]
    public List<Weather> GetLastWeatherRecords() => _weatherService.GetLastWeatherRecords();
}