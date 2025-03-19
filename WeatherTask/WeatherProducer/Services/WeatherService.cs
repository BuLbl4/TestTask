using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherProducer.Abstraction;
using WeatherProducer.Dto;
using WeatherProducer.Options;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WeatherProducer.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;
    private readonly WeatherOptions _weatherOptions;
    
    public WeatherService(HttpClient httpClient, IOptions<WeatherOptions> weatherOptions, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _weatherOptions = weatherOptions.Value;
    }

    public async Task<WeatherDto> FetchWeather()
    {
        var geoData = await FetchCoordinates();

        var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={geoData.Lat}&lon={geoData.Lon}&appid={_weatherOptions.Key}";
        
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {            
            throw new Exception();
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
        
        if (weatherData == null || weatherData.List.Count == 0)
        {
            throw new Exception("Нет данных");
        }

        var closestForecast = weatherData.List[0];
    
        var tempCelsius = closestForecast.Main.Temp - 273.15;
        
        return new WeatherDto
        {
            Temp = Math.Round(tempCelsius), 
            Description = closestForecast.Weather[0].Description
        };
    }
    
    private async Task<GeoData> FetchCoordinates()
    {
        var geoUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={_weatherOptions.City}&limit=5&appid={_weatherOptions.Key}";
        var geoResponse = await _httpClient.GetAsync(geoUrl);

        if (!geoResponse.IsSuccessStatusCode)
        {
            
            throw new Exception($"Ошибка при запросе координат: {geoResponse.StatusCode}");
        }

        var geoData = await geoResponse.Content.ReadAsStringAsync();
        
        var geoResults = JsonSerializer.Deserialize<GeoData[]>(geoData);

        if (geoResults == null || geoResults.Length == 0)
        {
            throw new Exception("Ошибка при десериализации");
        }
        _logger.LogInformation($"передали в кафку данные : {geoResults[0].Lat}, {geoResults[0].Lon}");
        return geoResults[0];
    }
}