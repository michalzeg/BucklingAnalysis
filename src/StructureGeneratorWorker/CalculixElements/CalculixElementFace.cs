using Shared.Contracts;

namespace StructureGeneratorWorker.CalculixElements;

public record struct CalculixElementFace
{
    public required PointD Vertex1 { get; init; }
    public required PointD Vertex2 { get; init; }
    public required PointD Vertex3 { get; init; }
}
