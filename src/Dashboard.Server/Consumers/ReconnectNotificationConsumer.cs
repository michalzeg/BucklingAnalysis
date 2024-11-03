using CalculationActivities.Activities;
using Dashboard.Server.Hubs;
using Infrastructure.MassTransit;
using MassTransit;
using Shared;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Consumers;

public class ReconnectNotificationConsumer : ConsumerBase<ReconnectNotification>
{
    private readonly IStorage _storage;
    private readonly IDashboardHubContext _dashboardHubContext;

    public ReconnectNotificationConsumer(ILogger<ReconnectNotificationConsumer> logger, IStorage storage, IDashboardHubContext dashboardHubContext) : base(logger)
    {
        _storage = storage;
        _dashboardHubContext = dashboardHubContext;
    }

    protected override async Task ConsumeHandle(ConsumeContext<ReconnectNotification> context)
    {
        await _storage.SetAsync(Constants.ConnectionId, context.Message.ConnectionId, context.Message.TrackingNumber);
        await _storage.SetAsync(context.Message.ConnectionId, context.Message.TrackingNumber);

        var completedActivityNames = await _storage.GetAsync<string[]>(StorageConstants.CompletedActivityNames, context.Message.TrackingNumber);
        if (completedActivityNames is null || completedActivityNames.Length == 0) {
            return;
        }

        foreach (var item in completedActivityNames)
        {
            await _dashboardHubContext.ActivityCompleted(context.Message.TrackingNumber, item);
        }

        if (completedActivityNames.Contains(nameof(GenerateFacadeActivity)))
        {
            await _dashboardHubContext.FacadeGenerated(context.Message.TrackingNumber);
        }
        if (completedActivityNames.Contains(nameof(FilterLinearAnalysisResultsActivity)))
        {
            await _dashboardHubContext.LinearAnalysisFinished(context.Message.TrackingNumber);
        }
        if (completedActivityNames.Contains(nameof(FilterBucklingAnalysisResultsActivity)))
        {
            await _dashboardHubContext.BucklingAnalysisFinished(context.Message.TrackingNumber);
        }
        if (completedActivityNames.Contains(nameof(FilterNonLinearAnalysisResultsActivity)))
        {
            await _dashboardHubContext.NonLinearAnalysisFinished(context.Message.TrackingNumber);
        }
    }
}
