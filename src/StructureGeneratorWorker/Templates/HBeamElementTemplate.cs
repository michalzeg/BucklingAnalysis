using Shared.Contracts;
using Shared.Contracts.Geometry;
using Shared.Contracts.Triangulation;
using Shared.Extensions;
using StructureGeneratorWorker.CalculixElements;
using StructureGeneratorWorker.Extensions;

namespace StructureGeneratorWorker.Templates;

public class HBeamElementTemplate : IElementTemplate
{
    public IEnumerable<ICalculixElement> GetConnectorElements(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        var yCoordinate = geometry.Width / 2d - geometry.WebThickness / 2d;
        var connectorNodes = triangles.Flange.Select(e => new[] { e.Vertex1, e.Vertex2, e.Vertex3 })
            .SelectMany(e => e)
            .Where(e => e.Y.IsApproximatelyEqualTo(yCoordinate))
            .OrderBy(e => e.X)
            .Select(e => new PointD() { X = e.X, Y = e.Y, Z = 0 })
            .Distinct()
            .ToList();

        for (int i = 0; i < connectorNodes.Count - 1; i++)
        {
            var current = connectorNodes[i];
            var next = connectorNodes[i + 1];
            var distance = next.X - current.X;

            var elementBottom = new C3D20(current, distance, geometry.WebThickness, geometry.FlangeThickness);
            var elementUp = elementBottom.Move(0, 0, geometry.Height - geometry.FlangeThickness);
            yield return elementBottom;
            yield return elementUp;
        }
    }

    public IEnumerable<ICalculixElement> GetFlangeElements(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        var mirrorAxis = geometry.Width / 2d;
        foreach (var flangeTriangle in triangles.Flange)
        {
            var normalizedTriangle = flangeTriangle.NormalizeVertices(crossProduct => crossProduct < 0);

            var p1 = new PointD() { X = flangeTriangle.Vertex1.X, Y = flangeTriangle.Vertex1.Y, Z = 0 };
            var p2 = new PointD() { X = flangeTriangle.Vertex2.X, Y = flangeTriangle.Vertex2.Y, Z = 0 };
            var p3 = new PointD() { X = flangeTriangle.Vertex3.X, Y = flangeTriangle.Vertex3.Y, Z = 0 };

            var distanceBetweenFlanges = geometry.Height - geometry.FlangeThickness;

            var elementBottomRight = new C3D15(p1, p2, p3, 0, 0, geometry.FlangeThickness);
            var elementBottomLeft = elementBottomRight.MirrorXZ(mirrorAxis);
            var elementUpperRight = elementBottomRight.Move(0, 0, distanceBetweenFlanges);
            var elementUpperLeft = elementBottomLeft.Move(0, 0, distanceBetweenFlanges);

            yield return elementBottomLeft;
            yield return elementBottomRight;
            yield return elementUpperLeft;
            yield return elementUpperRight;
        }
    }

    public IEnumerable<ICalculixElement> GetWebElements(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        foreach (var webTriangle in triangles.Web)
        {
            var normalizedTriangle = webTriangle.NormalizeVertices(crossProduct => crossProduct > 0);
            var dy = geometry.Width / 2d - geometry.WebThickness / 2d;
            var p1 = new PointD() { X = normalizedTriangle.Vertex1.X, Y = 0, Z = normalizedTriangle.Vertex1.Y }.Move(0, dy, geometry.FlangeThickness);
            var p2 = new PointD() { X = normalizedTriangle.Vertex2.X, Y = 0, Z = normalizedTriangle.Vertex2.Y }.Move(0, dy, geometry.FlangeThickness);
            var p3 = new PointD() { X = normalizedTriangle.Vertex3.X, Y = 0, Z = normalizedTriangle.Vertex3.Y }.Move(0, dy, geometry.FlangeThickness);
            var element = new C3D15(p1, p2, p3, 0, geometry.WebThickness, 0);
            yield return element;
        }
    }
}
