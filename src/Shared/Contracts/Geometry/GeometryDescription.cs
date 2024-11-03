namespace Shared.Contracts.Geometry;

public record GeometryDescription
{
    public required int FlangeThickness { get; init; }
    public required int WebThickness { get; init; }
    public required int Height { get; init; }
    public required int Width { get; init; }
    public required int Length { get; init; }
    public required double UniformLoad { get; init; }

    public required double Imperfection { get; init; }
}
