using Microsoft.Extensions.Configuration;

namespace Infrastructure.Redis;

public static class RedisConfig
{
    public static string? GetRedisConnection(this IConfiguration configuration) => configuration["Redis:Connection"];
}
