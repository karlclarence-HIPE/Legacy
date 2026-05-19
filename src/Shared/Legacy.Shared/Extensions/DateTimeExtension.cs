namespace Legacy.Shared.Extensions;
public static class DateTimeExtension
{
    public static bool IsValidDateTime(this DateTime dateTime)
    {
        return dateTime != default;
    }

    public static (DateTime, DateTime) GenerateFirstAndLastOfMonth(this DateTime dateRequested)
    {
        var firstDayOfMonth = new DateTime(dateRequested.Year, dateRequested.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        return (firstDayOfMonth, lastDayOfMonth);
    }
}
