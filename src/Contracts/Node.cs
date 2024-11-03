namespace Contracts
{
    public readonly record struct Node
    {
        public required NodeId Id { get; init; }
        public required Point Coordinates { get; init; }
    }
}
