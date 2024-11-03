namespace Shared.Contracts.Results;

public readonly record struct TriangleResult
{
    public NodeDisplacementResult Vertex1Displacements { get; init; }
    public NodeDisplacementResult Vertex2Displacements { get; init; }
    public NodeDisplacementResult Vertex3Displacements { get; init; }
    public NodeStressResult Vertex1Stresses { get; init; }
    public NodeStressResult Vertex2Stresses { get; init; }
    public NodeStressResult Vertex3Stresses { get; init; }
}
