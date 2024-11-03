using Shared.Contracts.Geometry;
using Shared.Contracts.Triangulation;
using StructureGeneratorWorker.CalculixElements;

namespace StructureGeneratorWorker.Templates;

public interface IElementTemplate
{
    IEnumerable<ICalculixElement> GetFlangeElements(GeometryDescription geometry, TriangulatedSegments triangles);
    IEnumerable<ICalculixElement> GetWebElements(GeometryDescription geometry, TriangulatedSegments triangles);
    IEnumerable<ICalculixElement> GetConnectorElements(GeometryDescription geometry, TriangulatedSegments triangles);
}
