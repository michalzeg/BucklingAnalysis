using Shared.Contracts.Facade;
using Shared.Contracts.Results;
using Shared.Extensions;
using Shared.Storage;
using System.ComponentModel.DataAnnotations;



namespace ResultFilterWorker.Filters;

public class BucklingAnalysisResultFilter : ResultFilterBase
{
    public BucklingAnalysisResultFilter(IStorage storage) : base(storage)
    {
    }

    public override async Task<FilteredNodeResults> Execute(Guid trackingNumber, NodeResults parsedResults)
    {
        var facade = await GetFacade(trackingNumber);
        var nodes = facade.Nodes.Select(e => e.Id).ToHashSet();
        var nodalDisplacements = parsedResults.Displacements.Where(e => nodes.Contains(e.Id)).ToDictionary(e => e.Id, f => f);

        var triangulateResults = facade.Faces.Select(e => new TriangleResult()
        {
            Vertex1Displacements = nodalDisplacements[e.Node1],
            Vertex2Displacements = nodalDisplacements[e.Node2],
            Vertex3Displacements = nodalDisplacements[e.Node3],
            Vertex1Stresses = new() { Id = e.Node1, Sxx = 0, Sxy = 0, Sxz = 0, Syy = 0, Syz = 0, Szz = 0 },
            Vertex2Stresses = new() { Id = e.Node2, Sxx = 0, Sxy = 0, Sxz = 0, Syy = 0, Syz = 0, Szz = 0 },
            Vertex3Stresses = new() { Id = e.Node3, Sxx = 0, Sxy = 0, Sxz = 0, Syy = 0, Syz = 0, Szz = 0 },
        }).ToArray();

        var maxDisplacement = parsedResults.Displacements.Max(e => e.AbsMax());

        return new FilteredNodeResults()
        {
            TriangleResults = triangulateResults,
            Sxx = new(),
            Syy = new(),
            Szz = new(),
            Sxy = new(),
            Sxz = new(),
            Syz = new(),
            MaxDisplacement = maxDisplacement,
        };
    }
}
