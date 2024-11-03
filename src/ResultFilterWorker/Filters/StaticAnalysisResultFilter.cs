using MathNet.Numerics.Statistics;
using Shared.Contracts.Facade;
using Shared.Contracts.Results;
using Shared.Extensions;
using Shared.Storage;
using Statistics = Shared.Contracts.Results.Statistics;



namespace ResultFilterWorker.Filters;

public class StaticAnalysisResultFilter : ResultFilterBase
{
    public StaticAnalysisResultFilter(IStorage storage) : base(storage)
    {
    }

    public override async Task<FilteredNodeResults> Execute(Guid trackingNumber, NodeResults parsedResults)
    {
        var facade = await GetFacade(trackingNumber);
        var nodes = facade.Nodes.Select(e => e.Id).ToHashSet();
        var nodalDisplacements = parsedResults.Displacements.Where(e => nodes.Contains(e.Id)).ToDictionary(e => e.Id, f => f);
        var nodalStresses = parsedResults.Stresses.Where(e => nodes.Contains(e.Id)).ToDictionary(e => e.Id, f => f);

        var triangulateResults = facade.Faces.Select(e => new TriangleResult()
        {
            Vertex1Displacements = nodalDisplacements[e.Node1],
            Vertex2Displacements = nodalDisplacements[e.Node2],
            Vertex3Displacements = nodalDisplacements[e.Node3],
            Vertex1Stresses = nodalStresses[e.Node1],
            Vertex2Stresses = nodalStresses[e.Node2],
            Vertex3Stresses = nodalStresses[e.Node3],
        }).ToArray();

        var pSxx = CalculateStatistics(nodalStresses.Select(e => e.Value.Sxx));
        var pSyy = CalculateStatistics(nodalStresses.Select(e => e.Value.Syy));
        var pSzz = CalculateStatistics(nodalStresses.Select(e => e.Value.Szz));
        var pSxy = CalculateStatistics(nodalStresses.Select(e => e.Value.Sxy));
        var pSxz = CalculateStatistics(nodalStresses.Select(e => e.Value.Sxz));
        var pSyz = CalculateStatistics(nodalStresses.Select(e => e.Value.Syz));

        var maxDisplacement = parsedResults.Displacements.Max(e => e.AbsMax());

        return new FilteredNodeResults()
        {
            TriangleResults = triangulateResults,
            Sxx = pSxx,
            Syy = pSyy,
            Szz = pSzz,
            Sxy = pSxy,
            Sxz = pSxz,
            Syz = pSyz,
            MaxDisplacement = maxDisplacement
        };
    }

    private static Statistics CalculateStatistics(IEnumerable<double> stresses)
    {
        var stress = stresses.ToArray();

        var p005 = stress.Percentile(5);
        var p095 = stress.Percentile(95);
        var max = stress.Max();
        var min = stress.Min();
        return new Statistics()
        {
            Percentile005 = p005,
            Percentile095 = p095,
            Min = min,
            Max = max
        };
    }
}



