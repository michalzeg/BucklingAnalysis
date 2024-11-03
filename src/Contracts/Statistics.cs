namespace Contracts
{
    public record struct Statistics
    {
        public double Max { get; init; }
        public double Min { get; init; }
        public double Percentile005 { get; init; }
        public double Percentile095 { get; init; }
    }
}
