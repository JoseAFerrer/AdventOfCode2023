namespace AocHelpers;

public static class CollectionExtensions
{
    /// <summary>
    /// Adds an IEnumerable to an already existing collection.
    /// </summary>
    /// <param name="list">Existing collection.</param>
    /// <param name="items">Items to add.</param>
    /// <typeparam name="T"></typeparam>
    public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items)
    {
        foreach (var item in items) list.Add(item);
    }
}