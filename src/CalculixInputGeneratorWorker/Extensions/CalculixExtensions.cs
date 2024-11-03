using Shared.Contracts;
using Shared.Contracts.Structure;

namespace CalculixInputGeneratorWorker.Extensions;

public static class CalculixExtensions
{
    public static string NormalizeToCalculix<T>(this IEnumerable<T> value)
    {
        if (value?.Any() != true)
        {
            return string.Empty;
        }

        var result = value.Select(e => e?.ToString() ?? string.Empty).Chunk(15)
            .Select(e => e.Aggregate((prev, next) => $"{prev}, {next}"))
            .Aggregate((prev, next) => $"{prev},{Environment.NewLine}{next}");
        return result;
    }

    public static string NormalizeToCalculix(this double value) => value.ToString("F6").Replace(",", ".");
    public static string NormalizeToCalculix(this int value) => value.ToString();

    public static string NormalizeToCalculix(this Node node) =>
        $"{node.Id.Value}, {node.Coordinates.X.NormalizeToCalculix()}, {node.Coordinates.Y.NormalizeToCalculix()}, {node.Coordinates.Z.NormalizeToCalculix()}";

    public static string NormalizeToCalculix(this Element element) =>
        new[] { element.Id.Value }.Concat(element.Nodes.Select(e => e.Id.Value)).NormalizeToCalculix();
}
