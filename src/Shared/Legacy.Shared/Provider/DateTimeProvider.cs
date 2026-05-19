
namespace Legacy.Shared.Provider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.Now;
}
