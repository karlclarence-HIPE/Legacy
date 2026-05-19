namespace Legacy.Shared.Utility;

public static class AuditMapper
{
    /// <summary>
    /// Maps a model to an audit log entry. 
    /// This method is a placeholder and should be implemented with the actual mapping logic based on the application's requirements.
    /// </summary>
    public static TResult? Map<TSource, TResult>(
        this TSource model, 
        Func<TSource, int?> getId, 
        Func<TSource, string?> getName, 
        Func<TSource, DateTime?> getDate,
        Func<int, string, DateTime, TResult> factory
        )
    {
        var id = getId(model);
        if (id == null) return default;

        var name = getName(model) ?? string.Empty;
        var date = getDate(model) ?? DateTime.Now;

        return factory(id.Value, name, date);
    }
}
