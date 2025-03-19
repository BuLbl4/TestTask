using WeatherProducer.Dto;

namespace WeatherProducer.Abstraction;

public interface IWeatherService
{
    public Task<WeatherDto> FetchWeather();
}