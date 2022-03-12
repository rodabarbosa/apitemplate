using ApiTemplate.Domain.Entities;
using ApiTemplate.Infra.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data;

public class ApiTemplateContext : DbContext
{
    public ApiTemplateContext(DbContextOptions<ApiTemplateContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        SetSeeds(builder);
        base.OnModelCreating(builder);
    }

    private void SetSeeds(ModelBuilder builder)
    {
        builder.Entity<WeatherForecast>().HasData(WeatherForecastSeed.GetSeeds());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}
