using Shared.Contracts.Results;

namespace ResultFilterWorker.Filters;

public interface IResultFilter
{
    Task<FilteredNodeResults> Execute(Guid trackingNumber, NodeResults parsedResults);
}
