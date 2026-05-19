namespace Legacy.Shared.Extensions;

public static class EnumerableExtensions
{
    public static T? Retrieve<T>(this IEnumerable<T>? source, Func<T, bool> condition, T? obj)
    {
        if (source is null) return obj;

        var enumerable = source.ToList();

        return enumerable.Count == 0 ? obj : enumerable.Where(condition).DefaultIfEmpty(obj).FirstOrDefault();
    }

    public static T? Retrieve<T>(this IEnumerable<T>? source, Func<T, Boolean> condition, Func<T, Boolean> nullCheck, T obj)
    {
        if (source is null) return obj;

        var enumerable = source.ToList();

        return enumerable.Count == 0 ? obj : enumerable.Where(nullCheck).Where(condition).DefaultIfEmpty(obj).FirstOrDefault();
    }

    /// <summary>
    /// Check if the container contains any elements.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="source">Source Enumerable</param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this IEnumerable<T>? source)
    {
        return source == null || !source.Any();
    }
}
