using System;
using System.Globalization;

namespace ApiTemplate.Application.Extensions;

public static class DateTimeExtension
{
    private static readonly CultureInfo _cultureInfo = new("en-US");
    public static DateTime ToDateTime(this string value) => DateTime.Parse(value, _cultureInfo);
}
