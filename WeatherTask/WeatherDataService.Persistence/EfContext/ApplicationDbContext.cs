using Microsoft.EntityFrameworkCore;
using WeatherDataService.Application.Interfaces;
using WeatherDataService.Domain.Entities;

namespace WeatherDataService.Persistence.EfContext;

public class ApplicationDbContext : DbContext, IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Weather> Weather { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Extensions.ServiceCollectionExtensions).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}