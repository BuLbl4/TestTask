using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WeatherDataService.Persistence.EfContext;

public class EfFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private readonly IConfiguration _configuration;

    public EfFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var config = new ConfigurationBuilder().Build();

        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionBuilder.UseNpgsql(connectionString);
        
        return new ApplicationDbContext(optionBuilder.Options);
    }
}