using Dashboard.Server.Hubs;
using Infrastructure.MassTransit;
using MassTransit;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Consumers;

public class NonLinearAnalysisResultsReadyNotificationConsumer : ConsumerBase<NonLinearAnalysisResultsReadyNotification>
{
    private readonly IDashboardHubContext _dashboardHubContext;

    public NonLinearAnalysisResultsReadyNotificationConsumer(ILogger<NonLinearAnalysisResultsReadyNotificationConsumer> logger, IDashboardHubContext dashboardHubContext) : base(logger)
    {
        _dashboardHubContext = dashboardHubContext;
    }

    protected override async Task ConsumeHandle(ConsumeContext<NonLinearAnalysisResultsReadyNotification> context)
    {
        await _dashboardHubContext.NonLinearAnalysisFinished(context.Message.TrackingNumber);
    }
}
