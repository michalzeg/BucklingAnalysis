namespace Shared.Contracts.Results;

public record NodeResults
{
    public required IReadOnlyCollection<NodeDisplacementResult> Displacements { get; init; } = [];
    public required IReadOnlyCollection<NodeStressResult> Stresses { get; init; } = [];
}
