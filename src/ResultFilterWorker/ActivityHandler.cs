using CalculationActivities;
using CalculationActivities.ActivityArguments;
using ResultFilterWorker.Filters;
using ResultFilterWorker.Storage;
using Shared;
using Shared.Contracts.Facade;
using Shared.Storage;


namespace ResultFilterWorker;

public class ActivityHandler : IActivityHandler
{
    private readonly IResultFilterProvider _resultFilterProvider;
    private readonly IStorageProvider _storageProvider;

    public ActivityHandler(IResultFilterProvider resultFilterProvider, IStorageProvider storageProvider)
    {
        _resultFilterProvider = resultFilterProvider;
        _storageProvider = storageProvider;
    }
    public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
    {
        var resultFilter = _resultFilterProvider.GetResultFilter(context.ActivityName);
        var results = await _storageProvider.GetResults(context.ActivityName, context.TrackingNumber);
        var filteredResults = await resultFilter.Execute(context.TrackingNumber, results);
        await _storageProvider.SetFilteredResults(context.ActivityName, filteredResults, context.TrackingNumber);

        return argument;
    }
}
