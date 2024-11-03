namespace Contracts
{
    public record FilteredNodeResults
    {
        public required IReadOnlyCollection<TriangleResult> TriangleResults { get; init; }

        public required Statistics Sxx { get; init; }
        public required Statistics Syy { get; init; }
        public required Statistics Szz { get; init; }
        public required Statistics Sxy { get; init; }
        public required Statistics Sxz { get; init; }
        public required Statistics Syz { get; init; }


    }
}
