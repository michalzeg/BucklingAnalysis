namespace Contracts
{
    public record Supports
    {
        public IReadOnlyCollection<NodeId> UX { get; init; } = [];
        public IReadOnlyCollection<NodeId> UY { get; init; } = [];
        public IReadOnlyCollection<NodeId> UZ { get; init; } = [];
    }
}
