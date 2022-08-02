using System;
using System.Collections.Generic;
using ApiTemplate.Application.Mappings;
using ApiTemplate.Domain.Entities;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Mappings;

public class ToDtoTest
{
    [Fact]
    public void Should_Map_To_Dto()
    {
        var entity = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            TemperatureC = 0,
            Summary = null
        };

        var dto = entity.ToDto();
        // Assert
        Assert.NotNull(dto);
    }

    [Fact]
    public void Should_Map_To_A_Null_Dto()
    {
        WeatherForecast? entity = default;

        var dto = entity.ToDto();
        // Assert
        Assert.Null(dto);
    }

    [Fact]
    public void Should_Map_To_A_Collection_Of_Dto()
    {
        var entities = new List<WeatherForecast>
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

        var dtos = entities.ToDtos();
        // Assert
        Assert.NotNull(dtos);
    }

    [Fact]
    public void Should_Map_To_A_Null_Collection_Of_Dto()
    {
        List<WeatherForecast>? entities = default;

        var dtos = entities.ToDtos();
        // Assert
        Assert.Null(dtos);
    }
}
