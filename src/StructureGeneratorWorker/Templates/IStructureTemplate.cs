using Shared.Contracts.Geometry;
using Shared.Contracts.Structure;
using Shared.Contracts.Triangulation;

namespace StructureGeneratorWorker.Templates;

public interface IStructureTemplate
{
    IEnumerable<Element> GetElements(GeometryDescription geometry, TriangulatedSegments triangles);
    IEnumerable<Load> GetLoads(GeometryDescription geometry, TriangulatedSegments triangles);
    Supports GetSupports(GeometryDescription geometry, TriangulatedSegments triangles);
}
