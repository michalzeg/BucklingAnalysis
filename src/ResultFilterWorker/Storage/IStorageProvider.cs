using Shared.Contracts.Results;



namespace ResultFilterWorker.Storage;

public interface IStorageProvider
{
    Task<NodeResults> GetResults(string activityName, Guid trackingNumber);
    Task SetFilteredResults(string activityName, FilteredNodeResults filteredResults, Guid trackingNumber);
}



