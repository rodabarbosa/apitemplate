using System;
using System.Globalization;

namespace ApiTemplate.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="DateTime"/>.
/// </summary>
public static class DateTimeExtension
{
    private static readonly CultureInfo _cultureInfo = new("en-US");

    /// <summary>
    /// Converts string to <see cref="DateTime"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string value) => DateTime.Parse(value, _cultureInfo);
}
