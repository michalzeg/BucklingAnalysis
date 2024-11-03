using Shared.Contracts.Triangulation;
using TriangleNet.Meshing;

namespace TriangulatorWorker.Extensions;

public static class TriangleNetExtensions
{
    public static Vertex Map(this TriangleNet.Geometry.Vertex vertex) => new() { X = vertex.X, Y = vertex.Y };
    public static Triangle Map(this TriangleNet.Topology.Triangle triangle)
    {
        var v1 = triangle.GetVertex(0);
        var v2 = triangle.GetVertex(1);
        var v3 = triangle.GetVertex(2);

        return new()
        {
            Vertex1 = v1.Map(),
            Vertex2 = v2.Map(),
            Vertex3 = v3.Map()
        };
    }

    public static IReadOnlyCollection<Triangle> Map(this IMesh mesh) => mesh.Triangles.Select(e => e.Map()).ToArray();
}
