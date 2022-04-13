using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Extensions;
using ApiTemplate.Application.Models;

namespace ApiTemplate.WebApi.Extensions;

public static class WeatherForecastResourceExtension
{
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

    public static OperationParam<int>? ExtractTemperatureCParam(this string param) => GetTemperature(param, "temperatureC");
    public static OperationParam<int>? ExtractTemperatureFParam(this string param) => GetTemperature(param, "temperatureF");

    private static OperationParam<int>? GetTemperature(string param, string key)
    {
        var lowercaseKey = key.ToLower();
        if (string.IsNullOrEmpty(param) || !param.ToLower().Contains($"{lowercaseKey}="))
            return default;

        string[] values = param.Split('&');
        foreach (string item in values)
        {
            if (!item.ToLower().StartsWith($"{lowercaseKey}="))
                continue;
            string[] date = item.Split('=');
            var operationParam = CleanParam(date[1]).Split(',');
            Operation operation = operationParam[0].ToOperation();
            int value = int.Parse(operationParam[1]);

            return new OperationParam<int>
            {
                Value = value,
                Operation = operation
            };
        }

        return default;
    }

    private static string CleanParam(string param) => param.TrimStart('[').TrimEnd(']');
}
