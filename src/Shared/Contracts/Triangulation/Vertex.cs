namespace Shared.Contracts.Triangulation;

public readonly record struct Vertex
{
    public required double X { get; init; }
    public required double Y { get; init; }
}
