namespace Shared.Contracts;

public readonly record struct Node
{
    public required NodeId Id { get; init; }
    public required PointD Coordinates { get; init; }
}
