using System;
using System.Collections.Generic;
using ApiTemplate.Application.Mappings;
using ApiTemplate.Application.Models;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Mappings;

public class ToEntityTest
{
    [Fact]
    public void Should_Map_To_Entity()
    {
        // Arrange
        var dto = new WeatherForecastDto
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            TemperatureC = 0,
            Summary = null
        };

        var entity = dto.ToEntity();

        Assert.NotNull(entity);
    }

    [Fact]
    public void Should_Map_To_Null_Entity()
    {
        // Arrange
        WeatherForecastDto? dto = default;

        var entity = dto.ToEntity();

        Assert.Null(entity);
    }

    [Fact]
    public void Should_Map_To_Collection_Of_Entity()
    {
        // Arrange
        var listDto = new List<WeatherForecastDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                TemperatureC = 0,
                Summary = null
            },
            new()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                TemperatureC = 0,
                Summary = null
            }
        };

        var entity = listDto.ToEntities();

        Assert.NotEmpty(entity);
    }

    [Fact]
    public void Should_Map_To_Null_Collection_Of_Entity()
    {
        // Arrange
        List<WeatherForecastDto>? listDto = default;

        var entity = listDto.ToEntities();

        Assert.Null(entity);
    }
}
