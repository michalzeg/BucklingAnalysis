using Shared.Contracts.Results;
using Shared.Contracts.Structure;
using Shared.Extensions;

namespace ImperfectionGeneratorWorker;

public static class ImperfectionGenerator
{
    public static StructureDetails ApplyBucklingShape(StructureDetails baseStructure, NodeResults nodeResults)
    {
        var nodeDisplacementMap = nodeResults.Displacements.ToDictionary(e => e.Id, f => f);

        var elements = new List<Element>(baseStructure.Elements.Count);

        foreach (var element in baseStructure.Elements)
        {
            var nodes = element.Nodes
                .Select(node => new { node, disp = nodeDisplacementMap[node.Id] })
                .Select(e => e.node.Move(e.disp.Dx, e.disp.Dy, e.disp.Dz))
                .ToArray();

            var modifiedElement = new Element()
            {
                ElementType = element.ElementType,
                Faces = element.Faces,
                Id = element.Id,
                Nodes = nodes,
            };
            elements.Add(modifiedElement);
        }

        return new StructureDetails()
        {
            Elements = elements,
            Loads = baseStructure.Loads,
            Supports = baseStructure.Supports
        };
    }
}
