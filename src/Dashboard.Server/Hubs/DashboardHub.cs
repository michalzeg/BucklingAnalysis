using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Shared.Contracts.Geometry;
using Shared.Events;
using Shared.Notifications;

namespace Dashboard.Server.Hubs;

public class DashboardHub : Hub<IDashboardHub>
{
    private readonly ILogger<DashboardHub> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public DashboardHub(ILogger<DashboardHub> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    public async Task Reconnect(Guid trackingNumber)
    {
        await _publishEndpoint.Publish(new ReconnectNotification(trackingNumber, Context.ConnectionId));
    }
    public async Task<Guid> StartCalculations(GeometryDescription geometry)
    {
        var trackingNumber = Guid.NewGuid();
        await _publishEndpoint.Publish<CalculationProcessRequested>(new(trackingNumber, geometry, Context.ConnectionId));

        _logger.LogInformation("Starting calculations for {trackingNumber} and {connectionId}", trackingNumber, Context.ConnectionId);
        return trackingNumber;
    }
}
