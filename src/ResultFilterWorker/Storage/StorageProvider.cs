using CalculationActivities.Activities;
using Shared;
using Shared.Contracts.Results;
using Shared.Storage;



namespace ResultFilterWorker.Storage;

public class StorageProvider : IStorageProvider
{
    private readonly IStorage _storage;

    public StorageProvider(IStorage storage)
    {
        _storage = storage;
    }
    public async Task<NodeResults> GetResults(string activityName, Guid trackingNumber)
    {
        var activityArgument = activityName switch
        {
            nameof(FilterLinearAnalysisResultsActivity) => ActivityArgumentType.LinearAnalysisResults,
            nameof(FilterBucklingAnalysisResultsActivity) => ActivityArgumentType.BucklingAnalysisResults,
            nameof(FilterNonLinearAnalysisResultsActivity) => ActivityArgumentType.NonLinearAnalysisResults,
            _ => throw new ArgumentException($"{activityName} is not a valid for result filter."),
        };

        var result = await _storage.GetAsync<NodeResults>(activityArgument, trackingNumber);
        return result;
    }

    public async Task SetFilteredResults(string activityName, FilteredNodeResults filteredResults, Guid trackingNumber)
    {
        var activityArgument = activityName switch
        {
            nameof(FilterLinearAnalysisResultsActivity) => ActivityArgumentType.FilteredLinearAnalysisResults,
            nameof(FilterBucklingAnalysisResultsActivity) => ActivityArgumentType.FilteredBucklingAnalysisResults,
            nameof(FilterNonLinearAnalysisResultsActivity) => ActivityArgumentType.FilteredNonLinearAnalysisResults,
            _ => throw new ArgumentException($"{activityName} is not a valid for result filter."),
        };
        await _storage.SetAsync(activityArgument, filteredResults, trackingNumber);
    }
}