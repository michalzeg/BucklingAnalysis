using System.Diagnostics.CodeAnalysis;
using Shared.Extensions;

namespace Shared.Contracts;

public readonly struct PointD : IEquatable<PointD>
{
    public required double X { get; init; }
    public required double Y { get; init; }
    public required double Z { get; init; }


    public bool Equals(PointD other) => X.IsApproximatelyEqualTo(other.X) && Y.IsApproximatelyEqualTo(other.Y) && Z.IsApproximatelyEqualTo(other.Z);
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is not null && Equals((PointD)obj);
    public override readonly int GetHashCode() => X.CalculateHashCode().GetHashCode() ^ Y.CalculateHashCode().GetHashCode() ^ Z.CalculateHashCode().GetHashCode();
    public static bool operator ==(PointD point1, PointD point2) => point1.Equals(point2);
    public static bool operator !=(PointD point1, PointD point2) => !(point1 == point2);
}