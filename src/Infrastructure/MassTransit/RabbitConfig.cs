namespace Infrastructure.MassTransit;

public record RabbitConfig
{
    public string? Password { get; init; }
    public string? Host { get; init; }
    public string? Username { get; init; }
    public string? VirtualHost { get; init; }
    public ushort Port { get; init; }
}
