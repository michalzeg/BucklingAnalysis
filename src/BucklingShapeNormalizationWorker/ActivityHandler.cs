using CalculationActivities;
using CalculationActivities.ActivityArguments;
using Shared;
using Shared.Contracts.Geometry;
using Shared.Contracts.Results;
using Shared.Storage;


namespace BucklingShapeNormalizationWorker;

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
        var geometry = await _storage.GetAsync<GeometryDescription>(nameof(ActivityArgumentType.Geometry), context.TrackingNumber);
        var bucklingResults = await _storage.GetAsync<NodeResults>(nameof(ActivityArgumentType.BucklingAnalysisResults), context.TrackingNumber);
        var result = BucklingShapeNormalizator.Run(geometry.Imperfection, bucklingResults);

        await _storage.SetAsync(nameof(ActivityArgumentType.NormalizedBucklingShape), result, context.TrackingNumber);
        return argument;
    }
}
