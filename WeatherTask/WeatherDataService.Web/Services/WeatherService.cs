using Grpc.Core;
using WeatherDataService.Application.Interfaces;
using WeatherService.Protos;

namespace WeatherDataService.Web.Services;


public class WeatherService : Weather.WeatherBase
{
    private readonly IDbContext _context;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IDbContext context, ILogger<WeatherService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public override async Task<SetWeatherResponse> SetWeather(SetWeatherRequest request, ServerCallContext context)
    {
        try
        {
            var weatherRecord = new Domain.Entities.Weather
            {
                Description = request.Description,
                Temp = request.Temp,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Time = TimeOnly.FromDateTime(DateTime.UtcNow)  
            };

            await _context.Weather.AddAsync(weatherRecord);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Данные успешно сохранены в базу");
            return new SetWeatherResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении данных в базу.");
            throw new RpcException(new Status(StatusCode.Internal, "Ошибка при сохранении данных."));
        }
    }

    public List<Domain.Entities.Weather> GetLastWeatherRecords()
    {
        return _context.Weather
            .OrderByDescending(w => w.Date)
            .ThenByDescending(w => w.Time)
            .Take(10)
            .ToList();
    }
}