namespace Legacy.Framework.Utility.Provider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.Now;
}
