using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherDataService.Domain.Entities;

namespace WeatherDataService.Persistence.Configurations;

public class WeatherConfiguration : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Description)
            .IsRequired();
        
        builder.Property(x => x.Temp)
            .IsRequired();
    }
}