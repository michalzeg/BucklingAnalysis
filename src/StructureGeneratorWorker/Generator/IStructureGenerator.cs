using Shared.Contracts.Geometry;
using Shared.Contracts.Structure;
using Shared.Contracts.Triangulation;

namespace StructureGeneratorWorker
{
    public interface IStructureGenerator
    {
        StructureDetails CreateElements(GeometryDescription geometry, TriangulatedSegments triangles);
    }
}