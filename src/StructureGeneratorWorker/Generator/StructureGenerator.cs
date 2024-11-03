using Shared.Contracts.Geometry;
using Shared.Contracts.Structure;
using Shared.Contracts.Triangulation;
using StructureGeneratorWorker.Templates;

namespace StructureGeneratorWorker;

public class StructureGenerator : IStructureGenerator
{
    private readonly IStructureTemplate _structureTemplate;

    public StructureGenerator(IStructureTemplate structureTemplate)
    {
        _structureTemplate = structureTemplate;
    }

    public StructureDetails CreateElements(GeometryDescription geometry, TriangulatedSegments triangles)
    {

        var structure = new StructureDetails()
        {
            Elements = _structureTemplate.GetElements(geometry, triangles).ToList(),
            Supports = _structureTemplate.GetSupports(geometry, triangles),
            Loads = _structureTemplate.GetLoads(geometry, triangles).ToList()
        };

        return structure;
    }


}
