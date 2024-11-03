using Shared.Contracts.Geometry;
using Shared.Contracts.Triangulation;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Smoothing;
using TriangulatorWorker.Extensions;

namespace TriangulatorWorker.Triangulator;


public static class TriangulatorFacade
{
    private const int _segmentCount = 10;

    public static TriangulatedSegments Triangulate(GeometryDescription geometryDescription)
    {

        var minDimension = Math.Min(Math.Min(geometryDescription.Width, geometryDescription.Height), geometryDescription.Length);
        var size = minDimension / _segmentCount;
        var minArea = geometryDescription.Length / 10 * geometryDescription.Height / 10;

        //web
        var x = geometryDescription.Length;
        var y = geometryDescription.Height - 2d * geometryDescription.FlangeThickness;
        var webTriangles = GetTriangles(x, y, size, minArea);

        //flange 
        y = geometryDescription.Width / 2d - geometryDescription.WebThickness / 2d;
        var flangeTriangles = GetTriangles(x, y, size, minArea);


        var result = new TriangulatedSegments() { Flange = flangeTriangles, Web = webTriangles };
        return result;
    }

    private static IReadOnlyCollection<Triangle> GetTriangles(double dx, double dy, int size, int minArea)
    {
        var rectangle = Generate.Rectangle(0, 0, dx, dy, size);
        var poly = new Polygon();
        poly.Add(rectangle);
        var webMesh = Triangulate(poly, minArea);
        return webMesh.Map();
    }

    private static IMesh Triangulate(Polygon poly, double minArea)
    {
        // Set minimum angle quality option.
        var quality = new QualityOptions()
        {
            MinimumAngle = 30.0,
            VariableArea = true,
            MaximumArea = minArea,
        };
        var constraintOptions = new ConstraintOptions()
        {
            SegmentSplitting = 1
        };
        // Generate mesh using the polygons Triangulate extension method.
        var mesh = poly.Triangulate(constraintOptions, quality);
        var smoother = new SimpleSmoother();

        // Smooth mesh.
        smoother.Smooth(mesh, 25, .05);
        return mesh;
    }
}
