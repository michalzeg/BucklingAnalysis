using Microsoft.Extensions.Configuration;

namespace Infrastructure.MassTransit;

public static class RabbitConfigExtensions
{
    private const string prefix = "rabbitmq://";

    public static RabbitConfig GetRabbitConfig(this IConfiguration configuration) => configuration.GetSection("RabbitMq").Get<RabbitConfig>()
        ?? throw new MissingFieldException("Could not find RabbitMq configuration");

    public static string VirtualHostNormalized(this RabbitConfig config) => config.VirtualHost == "/" ? "" : $"/{config.VirtualHost}";

    public static Uri GetQueueUri(this RabbitConfig config, string name) => new Uri($"{config.GetHost()}/{name}");

    public static string GetHost(this RabbitConfig config) => $"{prefix}{config.Host}{config.VirtualHostNormalized()}";
}