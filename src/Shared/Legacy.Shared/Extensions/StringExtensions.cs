using System.Globalization;

namespace Legacy.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsConvertibleToTime(this string source)
    {
        return DateTime.TryParseExact(source, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public static bool IsConvertibleToDate(this string source)
    {
        return DateTime.TryParseExact(source,
            "yyyyMMdd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None, out _);
    }

    public static DateTime ToDate(this string source)
    {
        DateTime.TryParseExact(source,
            "yyyyMMdd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var result);

        return result;
    }
}
