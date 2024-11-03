namespace Shared.Contracts.Facade;

public record FacadeDetails
{
    public required IReadOnlyCollection<TriangularFace> Faces { get; init; } = [];
    public required IReadOnlyCollection<Node> Nodes { get; init; } = [];
}
