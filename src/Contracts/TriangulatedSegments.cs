namespace Contracts;

public record TriangulatedSegments
{
    public required IReadOnlyCollection<Triangle> Web { get; init; } = [];
    public required IReadOnlyCollection<Triangle> Flange { get; init; } = [];
}
