using CalculationActivities;
using CalculationActivities.ActivityArguments;
using Newtonsoft.Json;
using Shared;
using Shared.Contracts.Structure;
using Shared.Storage;


namespace FacadeGeneratorWorker;

public class ActivityHandler : IActivityHandler
{
    private readonly IStorage _storage;

    public ActivityHandler(IStorage storage)
    {
        _storage = storage;
    }
    public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
    {
        var baseStructure = await _storage.GetAsync<StructureDetails>(ActivityArgumentType.BaseStructure, context.TrackingNumber);

        var facade = FacadeGenerator.Generate(baseStructure);

        await _storage.SetAsync(ActivityArgumentType.FacadeDetails, facade, context.TrackingNumber);

        return argument;
    }
}
