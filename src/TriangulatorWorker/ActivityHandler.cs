using CalculationActivities;
using CalculationActivities.ActivityArguments;
using Shared;
using Shared.Contracts.Geometry;
using Shared.Storage;
using TriangulatorWorker.Triangulator;

namespace TriangulatorWorker;

public class ActivityHandler : IActivityHandler
{
    private readonly ILogger<ActivityHandler> _logger;
    private readonly IStorage _storage;

    public ActivityHandler(ILogger<ActivityHandler> logger, IStorage storage)
    {
        _logger = logger;
        _storage = storage;
    }

    public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
    {
        var geometry = await _storage.GetAsync<GeometryDescription>(ActivityArgumentType.Geometry, context.TrackingNumber);
        var triangulatedSegments = TriangulatorFacade.Triangulate(geometry);

        await _storage.SetAsync(ActivityArgumentType.TriangulatedSegments, triangulatedSegments, context.TrackingNumber);

        return argument;
    }
}
