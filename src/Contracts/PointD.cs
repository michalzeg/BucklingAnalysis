using System.Diagnostics.CodeAnalysis;

namespace Contracts
{
    public readonly struct Point : IEquatable<Point>
    {

        public required int X { get; init; }
        public required int Y { get; init; }
        public required int Z { get; init; }

        public Point Move(int dx, int dy, int dz)
        {
            return new Point()
            {
                X = X + dx,
                Y = Y + dy,
                Z = Z + dz,
            };
        }

        public Point Move(double dx, double dy, double dz)
        {
            return new Point()
            {
                X = X + (int)dx,
                Y = Y + (int)dy,
                Z = Z + (int)dz,
            };
        }


        public Point MirrorXZ(int y)
        {
            var newY = Y <= y ? 2 * y - Y : y - Y;
            return new Point()
            {
                X = X,
                Y = newY,
                Z = Z
            };
        }

        public Point MiddlePoint(Point endPoint)
        {
            var dx = (endPoint.X - X) / 2d;
            var dy = (endPoint.Y - Y) / 2d;
            var dz = (endPoint.Z - Z) / 2d;

            var x = X + dx;
            var y = Y + dy;
            var z = Z + dz;
            return new Point()
            {
                X = (int)x,
                Y = (int)y,
                Z = (int)z,
            };
        }

        public double CrossProduct(Point point1, Point point2)
        {
            var vector10 = point1.X - X;
            var vector11 = point1.Y - Y;
            var vector20 = point2.X - point1.X;
            var vector21 = point2.Y - point1.Y;

            double result = (vector10 * vector21) - (vector11 * vector20);
            return result;
        }
        public bool Equals(Point other)
        {
            return X.IsApproximatelyEqualTo(other.X) && Y.IsApproximatelyEqualTo(other.Y) && Z.IsApproximatelyEqualTo(other.Z);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is null)
            {
                return false;
            }
            return Equals((Point)obj);
        }


        public override readonly int GetHashCode()
        {
            return X.CalculateHashCode().GetHashCode() ^ Y.CalculateHashCode().GetHashCode() ^ Z.CalculateHashCode().GetHashCode();
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }
    }
}