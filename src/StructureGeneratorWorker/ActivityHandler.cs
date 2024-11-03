using CalculationActivities;
using CalculationActivities.ActivityArguments;
using Shared;
using Shared.Contracts.Geometry;
using Shared.Contracts.Triangulation;
using Shared.Storage;

namespace StructureGeneratorWorker;

public class ActivityHandler : IActivityHandler
{
    private readonly ILogger<ActivityHandler> _logger;
    private readonly IStructureGenerator _structureGenerator;
    private readonly IStorage _storage;

    public ActivityHandler(ILogger<ActivityHandler> logger, IStructureGenerator structureGenerator, IStorage storage)
    {
        _logger = logger;
        _structureGenerator = structureGenerator;
        _storage = storage;
    }

    public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
    {
        var geometry = await _storage.GetAsync<GeometryDescription>(ActivityArgumentType.Geometry, context.TrackingNumber);
        var segments = await _storage.GetAsync<TriangulatedSegments>(ActivityArgumentType.TriangulatedSegments, context.TrackingNumber);
        var structure = _structureGenerator.CreateElements(geometry, segments);

        await _storage.SetAsync(ActivityArgumentType.BaseStructure, structure, context.TrackingNumber);

        return argument;
    }
}
