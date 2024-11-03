using Shared.Contracts.Facade;
using Shared.Contracts.Structure;

namespace FacadeGeneratorWorker;


public static class FacadeGenerator
{
    public static FacadeDetails Generate(StructureDetails structure)
    {
        var facadeFaces = structure.Elements
            .SelectMany(e => e.Faces)
            .Distinct()
            .OrderBy(e => e.Node1.Value)
            .ThenBy(e => e.Node2.Value)
            .ThenBy(e => e.Node3.Value)
            .ToArray();

        var externalNodes = facadeFaces
            .Select(e => new[] { e.Node1, e.Node2, e.Node3 })
            .SelectMany(e => e)
            .Distinct()
            .OrderBy(e => e.Value)
            .ToHashSet();


        var nodes = structure.Elements.SelectMany(e => e.Nodes).Where(e => externalNodes.Contains(e.Id)).Distinct().ToArray();

        return new FacadeDetails() { Nodes = nodes, Faces = facadeFaces };
    }
}
