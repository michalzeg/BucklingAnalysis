namespace Infrastructure.Extensions;

public static class GlobalExtensions
{
    public static string GetName<T>() => typeof(T).Name;

    public static string GetName(this object obj) => obj.GetType().Name;

    public static void AddRange<T>(this IList<T> list, IEnumerable<T> values)
    {
        foreach (var item in values)
        {
            list.Add(item);
        }
    }

    public static bool HasValue<T>(this IReadOnlyCollection<T>? collection) => collection?.Count > 0;

    public static List<T> AddMultiple<T>(this List<T> list, T val1, T val2, T val3)
    {
        list.Add(val1);
        list.Add(val2);
        list.Add(val3);
        return list;
    }
}
