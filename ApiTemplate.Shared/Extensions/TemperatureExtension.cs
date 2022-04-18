namespace ApiTemplate.Shared.Extensions;

public static class TemperatureExtension
{
    public static double ToFahrenheit(this double celsius) => celsius * 9 / 5 + 32;
}
