using ApiTemplate.Domain.Entities;
using ApiTemplate.Infra.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data;

public sealed class ApiTemplateContext : DbContext
{
    public ApiTemplateContext(DbContextOptions<ApiTemplateContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<WeatherForecast>? WeatherForecasts { get; set; }
    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetSeeds(modelBuilder);
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiTemplateContext).Assembly);
    }

    private static void SetSeeds(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>()
            .HasData(WeatherForecastSeed.GetSeeds());

        modelBuilder.Entity<User>()
            .HasData(UserSeed.GetSeeds());
    }
}
