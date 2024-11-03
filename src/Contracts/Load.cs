namespace Contracts
{
    public readonly record struct Load
    {
        public required NodeId Id { get; init; }
        public double FX { get; init; }
        public double FY { get; init; }
        public double FZ { get; init; }
    }
}
