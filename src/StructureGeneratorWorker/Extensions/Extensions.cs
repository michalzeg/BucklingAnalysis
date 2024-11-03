

using Shared.Contracts;
using Shared.Contracts.Structure;
using Shared.Contracts.Triangulation;
using Shared.Extensions;
using StructureGeneratorWorker.CalculixElements;

namespace StructureGeneratorWorker.Extensions;

public static class Extensions
{

    public static double CrossProduct(this Vertex point0, Vertex point1, Vertex point2)
    {
        var vector10 = point1.X - point0.X;
        var vector11 = point1.Y - point0.Y;
        var vector20 = point2.X - point1.X;
        var vector21 = point2.Y - point1.Y;

        double result = (vector10 * vector21) - (vector11 * vector20);
        return result;
    }

    public static double Area(this CalculixElementFace face)
    {
        var ax = face.Vertex1.X;
        var ay = face.Vertex1.Y;
        var bx = face.Vertex2.X;
        var by = face.Vertex2.Y;
        var cx = face.Vertex3.X;
        var cy = face.Vertex3.Y;

        var area = 0.5 * ((ax * (by - cy)) + (bx * (cy - ay)) + (cx * (ay - by)));
        return Math.Abs(area);
    }

    public static bool HasElevation(this CalculixElementFace face, double z) => face.Vertex1.HasElevation(z) && face.Vertex2.HasElevation(z) && face.Vertex3.HasElevation(z);

    public static ValueTuple<Load, Load, Load> GetFaceLoads(this CalculixElementFace face, double uniformLoad, Func<PointD, NodeId> nodeIdProvider)
    {
        var id1 = nodeIdProvider(face.Vertex1);
        var id2 = nodeIdProvider(face.Vertex2);
        var id3 = nodeIdProvider(face.Vertex3);

        var pointLoad = face.GetVertexLoad(uniformLoad);

        var l1 = new Load() { Id = id1, FZ = pointLoad };
        var l2 = new Load() { Id = id2, FZ = pointLoad };
        var l3 = new Load() { Id = id3, FZ = pointLoad };

        return new ValueTuple<Load, Load, Load>(l1, l2, l3);
    }

    public static double GetVertexLoad(this CalculixElementFace face, double uniformLoad)
    {
        var area = face.Area();
        var pointLoadPerVertex = uniformLoad * area / 3;
        return pointLoadPerVertex;
    }

    public static Triangle NormalizeVertices(this Triangle item, Func<double, bool> comparer)
    {
        var crossProduct = item.Vertex1.CrossProduct(item.Vertex2, item.Vertex3);
        if (comparer(crossProduct))
        {
            //reverse triangle
            return new Triangle() { Vertex1 = item.Vertex3, Vertex2 = item.Vertex2, Vertex3 = item.Vertex1 };
        }
        return item;
    }

}
