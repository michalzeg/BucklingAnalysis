using Shared.Contracts;
using Shared.Extensions;
namespace StructureGeneratorWorker.CalculixElements;
public class C3D20 : ICalculixElement
{
    private readonly PointD _origin;
    private readonly double _sizeX;
    private readonly double _sizeY;
    private readonly double _sizeZ;

    public C3D20(PointD origin, double size) : this(origin, size, size, size)
    {

    }

    public C3D20(PointD origin, double sizeX, double sizeY, double sizeZ)
    {
        _origin = origin;
        _sizeX = sizeX;
        _sizeY = sizeY;
        _sizeZ = sizeZ;
    }

    public PointD[] GetVertexCoordinates()
    {
        var baseCorners = new[]
        {
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X + _sizeX,
                Y = _origin.Y,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X + _sizeX,
                Y = _origin.Y + _sizeY,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y + _sizeY,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y,
                Z = _origin.Z + _sizeZ
            },
            new PointD()
            {
                X = _origin.X + _sizeX,
                Y = _origin.Y,
                Z = _origin.Z + _sizeZ
            },
            new PointD()
            {
                X = _origin.X + _sizeX,
                Y = _origin.Y + _sizeY,
                Z = _origin.Z + _sizeZ
            },
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y + _sizeY,
                Z = _origin.Z + _sizeZ
            },
        };
        var middlePoints = new[]
        {
            baseCorners[0].MiddlePoint(baseCorners[1]),
            baseCorners[1].MiddlePoint(baseCorners[2]),
            baseCorners[2].MiddlePoint(baseCorners[3]),
            baseCorners[0].MiddlePoint(baseCorners[3]),

            baseCorners[4].MiddlePoint(baseCorners[5]),
            baseCorners[5].MiddlePoint(baseCorners[6]),
            baseCorners[6].MiddlePoint(baseCorners[7]),
            baseCorners[4].MiddlePoint(baseCorners[7]),

            baseCorners[0].MiddlePoint(baseCorners[4]),
            baseCorners[1].MiddlePoint(baseCorners[5]),
            baseCorners[2].MiddlePoint(baseCorners[6]),
            baseCorners[3].MiddlePoint(baseCorners[7]),
        };

        return baseCorners.Concat(middlePoints).ToArray();
    }

    public C3D20 Move(int dx, int dy, int dz)
    {
        var newOrigin = _origin.Move(dx, dy, dz);



        return new C3D20(newOrigin, _sizeX, _sizeY, _sizeZ);
    }

    public PointD Center()
    {
        return new PointD()
        {
            X = _origin.X + _sizeX / 2,
            Y = _origin.Y + _sizeY / 2,
            Z = _origin.Z + _sizeZ / 2
        };
    }

    public (C3D15 upper, C3D15 lower) SplitToC3D15()
    {
        var up1 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y,
            Z = _origin.Z + _sizeZ
        };
        var up2 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y + _sizeY,
            Z = _origin.Z
        };
        var up3 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y + _sizeY,
            Z = _origin.Z + _sizeZ
        };

        var upper = new C3D15(up1, up2, up3, _sizeX, 0, 0);

        var lp1 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y + _sizeY,
            Z = _origin.Z
        };
        var lp2 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y,
            Z = _origin.Z + _sizeZ
        };
        var lp3 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y,
            Z = _origin.Z
        };
        var lower = new C3D15(lp1, lp2, lp3, _sizeX, 0, 0);
        return (upper, lower);
    }

    public (C3D6 upper, C3D6 lower) SplitToC3D6()
    {
        var up1 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y,
            Z = _origin.Z + _sizeZ
        };
        var up2 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y + _sizeY,
            Z = _origin.Z
        };
        var up3 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y + _sizeY,
            Z = _origin.Z + _sizeZ
        };

        var upper = new C3D6(up1, up2, up3, _sizeX, 0, 0);

        var lp1 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y + _sizeY,
            Z = _origin.Z
        };
        var lp2 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y,
            Z = _origin.Z + _sizeZ
        };
        var lp3 = new PointD()
        {
            X = _origin.X,
            Y = _origin.Y,
            Z = _origin.Z
        };
        var lower = new C3D6(lp1, lp2, lp3, _sizeX, 0, 0);
        return (upper, lower);
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
                    Vertex1 = nodes[0],
                    Vertex2 = nodes[2],
                    Vertex3 = nodes[3],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[4],
                    Vertex2 = nodes[5],
                    Vertex3 = nodes[6],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[4],
                    Vertex2 = nodes[6],
                    Vertex3 = nodes[7],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[0],
                    Vertex2 = nodes[1],
                    Vertex3 = nodes[4],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[1],
                    Vertex2 = nodes[4],
                    Vertex3 = nodes[5],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[2],
                    Vertex2 = nodes[3],
                    Vertex3 = nodes[7],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[2],
                    Vertex2 = nodes[6],
                    Vertex3 = nodes[7],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[1],
                    Vertex2 = nodes[3],
                    Vertex3 = nodes[4],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[3],
                    Vertex2 = nodes[4],
                    Vertex3 = nodes[7],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[1],
                    Vertex2 = nodes[2],
                    Vertex3 = nodes[5],
                },
                new CalculixElementFace()
                {
                    Vertex1 = nodes[2],
                    Vertex2 = nodes[5],
                    Vertex3 = nodes[6],
                },
            };
        return faces;
    }
}