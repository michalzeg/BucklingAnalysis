namespace Contracts;

public readonly record struct NodeDisplacementResult
{
    public required NodeId Id { get; init; }
    public required double Dx { get; init; }
    public required double Dy { get; init; }
    public required double Dz { get; init; }
}
