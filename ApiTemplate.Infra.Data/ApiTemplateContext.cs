using ApiTemplate.Domain.Entities;
using ApiTemplate.Infra.Data.Extensions;
using ApiTemplate.Infra.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data;

public class ApiTemplateContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ApiTemplateContext(DbContextOptions<ApiTemplateContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Database.EnsureCreated();
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetSeeds(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyAllConfigurations();
    }

    private static void SetSeeds(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>()
            .HasData(WeatherForecastSeed.GetSeeds());
    }
}
