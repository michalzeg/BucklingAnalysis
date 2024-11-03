using CalculixSolverWorker.Utils;
using MassTransit;
using Shared.Events;

namespace CalculixSolverWorker.Services;

public class NotificationService : INotificationService
{
    private readonly TimeSpan _delay;
    private readonly ILogger<NotificationService> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    private DateTimeOffset _lastNotificationTime = DateTimeOffset.UtcNow;

    public NotificationService(ILogger<NotificationService> logger, IConfiguration configuration, IPublishEndpoint publishEndpoint)
    {
        _delay = configuration.GetDelay();
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public void SendProgress(Guid trackingNumber, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (DateTimeOffset.UtcNow > _lastNotificationTime + _delay)
        {
            _logger.LogInformation(value);
            _publishEndpoint.Publish(new CalculixSolverProgress(trackingNumber, value));
            _lastNotificationTime = DateTimeOffset.UtcNow;
        }

    }

}
