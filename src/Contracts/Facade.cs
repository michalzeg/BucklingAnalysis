namespace Contracts
{
    public record Facade
    {
        public required IReadOnlyCollection<TriangularFace> Faces { get; init; } = [];
        public required IReadOnlyCollection<Node> Nodes { get; init; } = [];
    }
}
