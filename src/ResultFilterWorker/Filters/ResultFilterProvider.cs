using CalculationActivities.Activities;



namespace ResultFilterWorker.Filters;

public class ResultFilterProvider : IResultFilterProvider
{
    private readonly IServiceProvider _serviceProvider;

    public ResultFilterProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IResultFilter GetResultFilter(string activityName)
    {
        IResultFilter resultFilter = activityName switch
        {
            nameof(FilterLinearAnalysisResultsActivity) => _serviceProvider.GetRequiredService<StaticAnalysisResultFilter>(),
            nameof(FilterBucklingAnalysisResultsActivity) => _serviceProvider.GetRequiredService<BucklingAnalysisResultFilter>(),
            nameof(FilterNonLinearAnalysisResultsActivity) => _serviceProvider.GetRequiredService<StaticAnalysisResultFilter>(),
            _ => throw new ArgumentException($"{activityName} is not a valid for result filter."),
        };
        return resultFilter;
    }
}



