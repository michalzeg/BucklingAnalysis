using Shared.Contracts.Results;

namespace BucklingShapeNormalizationWorker;


public static class BucklingShapeNormalizator
{
    public static NodeResults Run(double imperfection, NodeResults nodeResults)
    {
        var displacements = nodeResults.Displacements.SelectMany(e => new[] { e.Dx, e.Dy, e.Dz }).ToList();
        var maxDisplacement = Math.Abs(displacements.Max());
        var minDisplacement = Math.Abs(displacements.Min());

        var absoluteDisplacement = Math.Max(maxDisplacement, minDisplacement);

        var factor = imperfection / absoluteDisplacement;

        var results = (from item in nodeResults.Displacements
                       let result = new NodeDisplacementResult()
                       {
                           Id = item.Id,
                           Dx = item.Dx * factor,
                           Dy = item.Dy * factor,
                           Dz = item.Dz * factor,
                       }
                       select result).ToList();
        return new NodeResults() { Displacements = results, Stresses = [] };
    }
}
