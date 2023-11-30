namespace IntelliTect.Multitool.Extensions;

/// <summary>
/// Various Linq extensions
/// </summary>
public static class SystemLinqExtensions
{
    /// <summary>
    /// Filters a sequence of values to only those that are not null.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input that are not null.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class?
    {
        return (IEnumerable<T>)source.Where(item => item is not null);
    }
}
