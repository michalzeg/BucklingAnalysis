using Shared.Contracts;
using System.Linq;

namespace StructureGeneratorWorker.CalculixElements;

public class C3D6 : ICalculixElement
{
    private readonly PointD _p1;
    private readonly PointD _p2;
    private readonly PointD _p3;
    private readonly double _dx;
    private readonly double _dy;
    private readonly double _dz;

    public C3D6(PointD p1, PointD p2, PointD p3, double dx, double dy, double dz)
    {
        _p1 = p1;
        _p2 = p2;
        _p3 = p3;
        _dx = dx;
        _dy = dy;
        _dz = dz;
    }

    public PointD[] GetVertexCoordinates()
    {
        //Console.WriteLine($"Selecting nodes for {_origin.X} {_origin.Y} {_origin.Z}");
        var baseCorners = new[]
        {
        _p1,
        _p2,
        _p3,
        new PointD()
        {
            X = _p1.X + _dx,
            Y = _p1.Y + _dy,
            Z = _p1.Z + _dz,
        },
        new PointD()
        {
            X = _p2.X + _dx,
            Y = _p2.Y + _dy,
            Z = _p2.Z + _dz,
        },
        new PointD()
        {
            X = _p3.X + _dx,
            Y = _p3.Y + _dy,
            Z = _p3.Z + _dz,
        }
    };


        return baseCorners;
    }

    public PointD Center()
    {
        return new PointD()
        {
            X = (_p1.X + _p2.X + _p3.X) / 3,
            Y = (_p1.Y + _p2.Y + _p3.Y) / 3,
            Z = (_p1.Z + _p2.Z + _p3.Z) / 3,
        };
    }

    public CalculixElementFace[] GetCalculixElementFaces()
    {
        var nodes = GetVertexCoordinates();
        var faces = new[]
        {
            new CalculixElementFace()
            {
                Vertex1 = nodes[0],
                Vertex2 = nodes[1],
                Vertex3 = nodes[2],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[3],
                Vertex2 = nodes[4],
                Vertex3 = nodes[5],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[0],
                Vertex2 = nodes[1],
                Vertex3 = nodes[3],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[1],
                Vertex2 = nodes[3],
                Vertex3 = nodes[4],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[0],
                Vertex2 = nodes[2],
                Vertex3 = nodes[3],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[2],
                Vertex2 = nodes[3],
                Vertex3 = nodes[5],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[0],
                Vertex2 = nodes[1],
                Vertex3 = nodes[3],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[1],
                Vertex2 = nodes[2],
                Vertex3 = nodes[5],
            },
            new CalculixElementFace()
            {
                Vertex1 = nodes[1],
                Vertex2 = nodes[4],
                Vertex3 = nodes[5],
            },
        };
        return faces;
    }
}