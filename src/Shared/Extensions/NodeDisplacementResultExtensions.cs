using Shared.Contracts.Results;

namespace Shared.Extensions;

public static class NodeDisplacementResultExtensions
{
    public static double AbsMax(this NodeDisplacementResult nodeDisplacementResult)
    {
        var absDx = Math.Abs(nodeDisplacementResult.Dx);
        var absDy = Math.Abs(nodeDisplacementResult.Dy);
        var absDz = Math.Abs(nodeDisplacementResult.Dz);
        return Math.Max(Math.Max(absDx, absDy), absDz);
    }
}