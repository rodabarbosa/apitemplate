using ApiTemplate.Application.Models;
using System;

namespace ApiTemplate.Application.Extensions;

/// <summary>
/// WeatherForecastResource Extension
/// </summary>
public static class WeatherForecastResourceExtension
{
    /// <summary>
    /// Extracts date time param
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public static OperationParam<DateTime>? ExtractDateParam(this string? param)
    {
        if (string.IsNullOrEmpty(param?.Trim()) || !param.ToLower().Contains("date="))
            return default;

        var values = param.Split('&');
        foreach (var item in values)
        {
            if (!item.ToLower().StartsWith("date="))
                continue;

            var date = item.Split('=');
            var operationParam = CleanParam(date[1]).Split(',');
            var operation = operationParam[0].ToOperation();
            var dateTime = operationParam[1].ToDateTime();

            return new OperationParam<DateTime>
            {
                Value = dateTime,
                Operation = operation
            };
        }

        return default;
    }

    /// <summary>
    /// Extracts the temperature celsius param.
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public static OperationParam<decimal>? ExtractTemperatureCelsiusParam(this string? param)
    {
        return GetTemperature(param, "temperatureCelsius");
    }

    /// <summary>
    /// Extracts the temperature fahrenheit param.
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public static OperationParam<decimal>? ExtractTemperatureFahrenheitParam(this string param)
    {
        return GetTemperature(param, "temperatureFahrenheit");
    }

    private static OperationParam<decimal>? GetTemperature(string? param, string key)
    {
        var lowercaseKey = key.ToLower();
        if (string.IsNullOrEmpty(param?.Trim()))
            return default;

        var values = param.Split('&');
        foreach (var item in values)
        {
            if (!item.StartsWith($"{lowercaseKey}=", StringComparison.OrdinalIgnoreCase))
                continue;

            var data = item.Split('=');
            var operationParam = CleanParam(data[1]).Split(',');
            var operation = operationParam[0].ToOperation();

            _ = decimal.TryParse(operationParam[1], out var value);

            return new OperationParam<decimal>
            {
                Value = value,
                Operation = operation
            };
        }

        return default;
    }

    private static string CleanParam(string param)
    {
        return param.TrimStart('[').TrimEnd(']');
    }
}
