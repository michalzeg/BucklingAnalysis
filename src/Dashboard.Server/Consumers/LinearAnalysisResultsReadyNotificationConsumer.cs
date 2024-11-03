using CalculationActivities.ActivityArguments;
using Dashboard.Server.Hubs;
using Infrastructure.MassTransit;
using MassTransit;
using Shared.Contracts.Results;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Consumers;

public class LinearAnalysisResultsReadyNotificationConsumer : ConsumerBase<LinearAnalysisResultsReadyNotification>
{
    private readonly IDashboardHubContext _dashboardHubContext;

    public LinearAnalysisResultsReadyNotificationConsumer(ILogger<LinearAnalysisResultsReadyNotificationConsumer> logger, IDashboardHubContext dashboardHubContext) : base(logger)
    {
        _dashboardHubContext = dashboardHubContext;
    }

    protected override async Task ConsumeHandle(ConsumeContext<LinearAnalysisResultsReadyNotification> context)
    {
        await _dashboardHubContext.LinearAnalysisFinished(context.Message.TrackingNumber);
    }
}
