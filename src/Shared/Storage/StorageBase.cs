
namespace Shared.Storage;

public abstract class StorageBase : IStorage
{
    protected static string GetFullName(Guid trackingNumber, string name) => trackingNumber == Guid.Empty ? name : $"{trackingNumber}:{name}";
    public abstract Task<T> GetAsync<T>(string name, Guid trackingNumber = new Guid());
    public abstract Task SetAsync<T>(string name, T obj, Guid trackingNumber = new Guid());
    public abstract Task<T> GetAsync<T>(ActivityArgumentType name, Guid trackingNumber = new Guid());
    public abstract Task SetAsync<T>(ActivityArgumentType name, T obj, Guid trackingNumber = new Guid());

    public abstract Task<Stream> GetStreamAsync(string name, Guid trackingNumber = new Guid());
}
