using System.Text.Json.Serialization;

namespace WeatherProducer.Dto;


public class GeoData
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}