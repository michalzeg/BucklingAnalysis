using Microsoft.AspNetCore.SignalR;
using Shared;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Hubs;

public class DashboardHubContext : IDashboardHubContext
{
    private readonly ILogger<DashboardHubContext> _logger;
    private readonly IStorage _storage;
    private readonly IHubContext<DashboardHub, IDashboardHub> _hubContext;

    public DashboardHubContext(ILogger<DashboardHubContext> logger, IStorage storage, IHubContext<DashboardHub, IDashboardHub> hubContext)
    {
        _logger = logger;
        _storage = storage;
        _hubContext = hubContext;
    }

    public async Task ActivityCompleted(Guid trackingNumber, string activityName)
    {
        var connection = await GetConnectionId(trackingNumber);

        await _hubContext.Clients.Client(connection).ActivityCompleted(activityName);
    }

    public async Task ReportCalculixProgress(Guid trackingNumber, CalculixProgressValue progress)
    {
        var connection = await GetConnectionId(trackingNumber);

        await _hubContext.Clients.Client(connection).ReportCalculixProgress(progress);
    }

    public async Task FacadeGenerated(Guid trackingNumber)
    {
        var connection = await GetConnectionId(trackingNumber);

        await _hubContext.Clients.Client(connection).FacadeGenerated();
    }

    public async Task LinearAnalysisFinished(Guid trackingNumber)
    {
        var connection = await GetConnectionId(trackingNumber);

        await _hubContext.Clients.Client(connection).LinearAnalysisFinished();
    }

    public async Task BucklingAnalysisFinished(Guid trackingNumber)
    {
        var connection = await GetConnectionId(trackingNumber);

        await _hubContext.Clients.Client(connection).BucklingAnalysisFinished();
    }

    public async Task NonLinearAnalysisFinished(Guid trackingNumber)
    {
        var connection = await GetConnectionId(trackingNumber);

        await _hubContext.Clients.Client(connection).NonLinearAnalysisFinished();
    }

    private async Task<string> GetConnectionId(Guid trackingNumber)
    {
        var connection = await _storage.GetAsync<string>(Constants.ConnectionId, trackingNumber);
        var connectionTrackingNumber = await _storage.GetAsync<Guid>(connection);
        if (string.IsNullOrWhiteSpace(connection) || trackingNumber != connectionTrackingNumber)
        {
            _logger.LogWarning("Connection for {trackingNumber} not found", trackingNumber);
            return string.Empty;
        }

        return connection;
    }
}
