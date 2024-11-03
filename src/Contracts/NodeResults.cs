namespace Contracts
{
    public record NodeResults
    {
        public required IReadOnlyCollection<NodeDisplacementResult> Displacements { get; init; } = [];
        public required IReadOnlyCollection<NodeStressResult> Stresses { get; init; } = [];
    }
}
