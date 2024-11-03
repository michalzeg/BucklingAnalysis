using CalculationActivities;
using CalculationActivities.ActivityArguments;
using Newtonsoft.Json;
using Shared;
using Shared.Contracts.Results;
using Shared.Contracts.Structure;
using Shared.Storage;
using System;


namespace ImperfectionGeneratorWorker;

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
        var baseStructure = await _storage.GetAsync<StructureDetails>(ActivityArgumentType.BaseStructure, context.TrackingNumber);
        var imperfectedShape = await _storage.GetAsync<NodeResults>(ActivityArgumentType.NormalizedBucklingShape, context.TrackingNumber);
        var result = ImperfectionGenerator.ApplyBucklingShape(baseStructure, imperfectedShape);
        await _storage.SetAsync(ActivityArgumentType.ImperfectedStructure, result, context.TrackingNumber);
        return argument;
    }
}
