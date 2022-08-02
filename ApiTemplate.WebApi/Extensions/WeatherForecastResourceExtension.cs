using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Extensions;
using ApiTemplate.Application.Models;

namespace ApiTemplate.WebApi.Extensions;

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
    public static OperationParam<DateTime>? ExtractDateParam(this string param)
    {
        if (string.IsNullOrEmpty(param) || !param.ToLower().Contains("date="))
            return default;

        string[] values = param.Split('&');
        foreach (string item in values)
        {
            if (!item.ToLower().StartsWith("date="))
                continue;

            string[] date = item.Split('=');
            var operationParam = CleanParam(date[1]).Split(',');
            Operation operation = operationParam[0].ToOperation();
            DateTime dateTime = operationParam[1].ToDateTime();

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
    public static OperationParam<double>? ExtractTemperatureCelsiusParam(this string param) => GetTemperature(param, "temperatureC");

    /// <summary>
    /// Extracts the temperature fahrenheit param.
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public static OperationParam<double>? ExtractTemperatureFahrenheitParam(this string param) => GetTemperature(param, "temperatureF");

    private static OperationParam<double>? GetTemperature(string param, string key)
    {
        var lowercaseKey = key.ToLower();
        if (string.IsNullOrEmpty(param) || !param.ToLower().Contains($"{lowercaseKey}="))
            return default;

        string[] values = param.Split('&');
        foreach (string item in values)
        {
            if (!item.ToLower().StartsWith($"{lowercaseKey}="))
                continue;

            string[] data = item.Split('=');
            var operationParam = CleanParam(data[1]).Split(',');
            Operation operation = operationParam[0].ToOperation();

            double.TryParse(operationParam[1], out double value);

            return new OperationParam<double>
            {
                Value = value,
                Operation = operation
            };
        }

        return default;
    }

    private static string CleanParam(string param) => param.TrimStart('[').TrimEnd(']');
}
