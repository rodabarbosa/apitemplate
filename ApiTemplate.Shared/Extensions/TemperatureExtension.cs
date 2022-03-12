namespace ApiTemplate.Shared.Extensions;

public static class TemperatureExtension
{
    public static int ToFahrenheit(this int celsius) => celsius * 9 / 5 + 32;
}
