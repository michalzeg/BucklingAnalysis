namespace Contracts;

public readonly record struct Triangle
{
    public required Vertex Vertex1 { get; init; }
    public required Vertex Vertex2 { get; init; }
    public required Vertex Vertex3 { get; init; }
}
