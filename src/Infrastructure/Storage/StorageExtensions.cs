using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Storage;

namespace Infrastructure.Storage;

public static class StorageExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStorage, WebStorage>();
        services.AddHttpClient<WebStorage>();
        return services;
    }
}