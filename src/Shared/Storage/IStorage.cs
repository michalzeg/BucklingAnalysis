namespace Shared.Storage;

public interface IStorage
{
    Task<Stream> GetStreamAsync(string name, Guid trackingNumber = new Guid());

    Task<T> GetAsync<T>(string name, Guid trackingNumber = new Guid());
    Task SetAsync<T>(string name, T obj, Guid trackingNumber = new Guid());
    Task<T> GetAsync<T>(ActivityArgumentType name, Guid trackingNumber = new Guid());
    Task SetAsync<T>(ActivityArgumentType name, T obj, Guid trackingNumber = new Guid());
}