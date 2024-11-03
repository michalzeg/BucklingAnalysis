using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Redis;

public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration, string? name = null)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetRedisConnection();
            options.InstanceName = name;
        });
        return services;
    }
}
