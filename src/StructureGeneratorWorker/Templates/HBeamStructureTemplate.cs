using Infrastructure.Extensions;
using Shared.Contracts;
using Shared.Contracts.Facade;
using Shared.Contracts.Geometry;
using Shared.Contracts.Structure;
using Shared.Contracts.Triangulation;
using Shared.Extensions;
using StructureGeneratorWorker.CalculixElements;
using StructureGeneratorWorker.Extensions;

namespace StructureGeneratorWorker.Templates;

public class HBeamStructureTemplate : IStructureTemplate
{
    private readonly IElementTemplate _elementTemplate;
    private Dictionary<PointD, NodeId>? _nodeNumberMap;
    private List<ICalculixElement>? _calculixElements;
    public HBeamStructureTemplate(IElementTemplate elementTemplate)
    {
        _elementTemplate = elementTemplate;
    }

    public IEnumerable<Element> GetElements(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        Initialize(geometry, triangles);

        var elements = _calculixElements!.Select((element, index) =>
        {
            var nodes = element.GetVertexCoordinates().Select(e => new Node() { Coordinates = e, Id = _nodeNumberMap![e] }).ToList();
            var faces = element.GetCalculixElementFaces().Select(e => new TriangularFace() { Node1 = _nodeNumberMap![e.Vertex1], Node2 = _nodeNumberMap[e.Vertex2], Node3 = _nodeNumberMap[e.Vertex3] }).ToList();
            return new Element()
            {
                Id = new ElementId(index + 1),
                ElementType = Enum.Parse<ElementType>(element.GetType().Name),
                Faces = faces,
                Nodes = nodes,
            };
        });
        return elements;
    }

    public IEnumerable<Load> GetLoads(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        Initialize(geometry, triangles);
        return _calculixElements!
                    .Select(e => e.GetCalculixElementFaces())
                    .SelectMany(e => e)
                    .Where(e => e.HasElevation(geometry.Height))
                    .Select(e => e.GetFaceLoads(geometry.UniformLoad, e => _nodeNumberMap![e]))
                    .Aggregate(new List<Load>(), (prev, next) => prev.AddMultiple(next.Item1, next.Item2, next.Item3));
    }

    public Supports GetSupports(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        Initialize(geometry, triangles);
        var minBX = _nodeNumberMap!.Select(e => e.Key.X).OrderBy(e => e).Take(1).ToList();
        var maxBX = _nodeNumberMap!.Select(e => e.Key.X).OrderByDescending(e => e).Take(1).ToList();

        var boundaryNumbersLeft = _nodeNumberMap!.Where(e => e.Key.Z.IsApproximatelyEqualTo(0) && minBX.Any(g => g.IsApproximatelyEqualTo(e.Key.X)))
            .Select(e => e.Value).Distinct().ToArray();

        var boundaryNumbersRight = _nodeNumberMap!.Where(e => e.Key.Z.IsApproximatelyEqualTo(0) && maxBX.Any(g => g.IsApproximatelyEqualTo(e.Key.X)))
            .Select(e => e.Value).Distinct().ToArray();

        var supportsUZ = boundaryNumbersLeft.Concat(boundaryNumbersRight).ToArray();

        var supportsUX = boundaryNumbersLeft.ToArray();

        var maxY = _nodeNumberMap!.Max(e => e.Key.Y);
        var supportsUY = _nodeNumberMap!.Where(e => e.Key.Z.IsApproximatelyEqualTo(0) && e.Key.Y.IsApproximatelyEqualTo(maxY))
            .Select(e => e.Value).Distinct().ToArray();

        var supports = new Supports()
        {
            UX = supportsUX,
            UY = supportsUY,
            UZ = supportsUZ
        };
        return supports;
    }

    private void Initialize(GeometryDescription geometry, TriangulatedSegments triangles)
    {
        if (_calculixElements.HasValue() && _nodeNumberMap.HasValue())
        {
            return;
        }

        var webElements = _elementTemplate.GetWebElements(geometry, triangles);
        var flangeElements = _elementTemplate.GetFlangeElements(geometry, triangles);
        var connectorElements = _elementTemplate.GetConnectorElements(geometry, triangles);

        _calculixElements = webElements.Concat(flangeElements).Concat(connectorElements).ToList();

        _nodeNumberMap = _calculixElements!.SelectMany(e => e.GetVertexCoordinates())
            .Distinct()
            .Select((e, i) => new { point = e, index = i + 1 })
            .ToDictionary(e => e.point, f => new NodeId(f.index));
    }
}
