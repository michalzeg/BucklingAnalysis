using Shared.Contracts;
namespace StructureGeneratorWorker.CalculixElements;
public class C3D8
{
    private readonly PointD _origin;
    private readonly int _size;

    public C3D8(PointD origin, int size)
    {
        _origin = origin;
        _size = size;
    }

    public PointD[] GetNodes()
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
                X = _origin.X + _size,
                Y = _origin.Y,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X + _size,
                Y = _origin.Y + _size,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y + _size,
                Z = _origin.Z
            },
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y,
                Z = _origin.Z + _size
            },
            new PointD()
            {
                X = _origin.X + _size,
                Y = _origin.Y,
                Z = _origin.Z + _size
            },
            new PointD()
            {
                X = _origin.X + _size,
                Y = _origin.Y + _size,
                Z = _origin.Z + _size
            },
            new PointD()
            {
                X = _origin.X,
                Y = _origin.Y + _size,
                Z = _origin.Z + _size
            },
        };

        return baseCorners;
    }
}
