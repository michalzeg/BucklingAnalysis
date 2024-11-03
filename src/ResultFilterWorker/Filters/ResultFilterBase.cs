using Shared;
using Shared.Contracts.Facade;
using Shared.Contracts.Results;
using Shared.Storage;

namespace ResultFilterWorker.Filters;

public abstract class ResultFilterBase : IResultFilter
{
    private readonly IStorage _storage;

    protected ResultFilterBase(IStorage storage)
    {
        _storage = storage;
    }

    public abstract Task<FilteredNodeResults> Execute(Guid trackingNumber, NodeResults parsedResults);

    protected async Task<FacadeDetails> GetFacade(Guid trackingNumber) => await _storage.GetAsync<FacadeDetails>(ActivityArgumentType.FacadeDetails, trackingNumber);
}
