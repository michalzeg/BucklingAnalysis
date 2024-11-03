namespace Shared.Contracts.Results;

public readonly record struct NodeStressResult
{
    public required NodeId Id { get; init; }
    public required double Sxx { get; init; }
    public required double Syy { get; init; }
    public required double Szz { get; init; }
    public required double Sxy { get; init; }
    public required double Sxz { get; init; }
    public required double Syz { get; init; }
}
