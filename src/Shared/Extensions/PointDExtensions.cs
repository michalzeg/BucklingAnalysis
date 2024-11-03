using Shared.Contracts;

namespace Shared.Extensions;

public static class PointDExtensions
{
    public static bool HasElevation(this PointD point, double z) => point.Z.IsApproximatelyEqualTo(z);

    public static PointD Move(this PointD value, int dx, int dy, int dz)
    {
        return new PointD()
        {
            X = value.X + dx,
            Y = value.Y + dy,
            Z = value.Z + dz,
        };
    }

    public static PointD Move(this PointD value, double dx, double dy, double dz)
    {
        return new PointD()
        {
            X = value.X + dx,
            Y = value.Y + dy,
            Z = value.Z + dz,
        };
    }

    public static PointD MirrorXZ(this PointD value, int y)
    {
        var newY = value.Y <= y ? (2 * y) - value.Y : y - value.Y;
        return new PointD()
        {
            X = value.X,
            Y = newY,
            Z = value.Z
        };
    }

    public static PointD MirrorXZ(this PointD value, double y)
    {
        var newY = value.Y <= y ? (2 * y) - value.Y : y - value.Y;
        return new PointD()
        {
            X = value.X,
            Y = newY,
            Z = value.Z
        };
    }

    public static PointD MiddlePoint(this PointD value, PointD endPoint)
    {
        var dx = (endPoint.X - value.X) / 2d;
        var dy = (endPoint.Y - value.Y) / 2d;
        var dz = (endPoint.Z - value.Z) / 2d;

        var x = value.X + dx;
        var y = value.Y + dy;
        var z = value.Z + dz;
        return new PointD()
        {
            X = (int)x,
            Y = (int)y,
            Z = (int)z,
        };
    }

    public static double CrossProduct(this PointD value, PointD point1, PointD point2)
    {
        var vector10 = point1.X - value.X;
        var vector11 = point1.Y - value.Y;
        var vector20 = point2.X - point1.X;
        var vector21 = point2.Y - point1.Y;

        double result = (vector10 * vector21) - (vector11 * vector20);
        return result;
    }
}
