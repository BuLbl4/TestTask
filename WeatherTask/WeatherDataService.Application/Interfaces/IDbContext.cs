using Microsoft.EntityFrameworkCore;
using WeatherDataService.Domain.Entities;

namespace WeatherDataService.Application.Interfaces;

public interface IDbContext
{
    public DbSet<Weather> Weather { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}