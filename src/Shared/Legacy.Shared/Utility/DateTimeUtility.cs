namespace Legacy.Shared.Utility;

public abstract class DateTimeUtility
{
    public static IEnumerable<DateTime> CalendarDay(DateTime startDate, DateTime endDate)
    {
        for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
    }

}
