namespace Contracts
{
    public record struct TriangularFace
    {
        public required NodeId Node1 { get; init; }
        public required NodeId Node2 { get; init; }
        public required NodeId Node3 { get; init; }
    }
}
