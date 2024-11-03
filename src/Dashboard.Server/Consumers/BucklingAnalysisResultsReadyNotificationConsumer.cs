using CalculationActivities.ActivityArguments;
using Dashboard.Server.Hubs;
using Infrastructure.MassTransit;
using MassTransit;
using Shared.Contracts.Results;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Consumers;

public class BucklingAnalysisResultsReadyNotificationConsumer : ConsumerBase<BucklingAnalysisReadyNotification>
{
    private readonly IDashboardHubContext _dashboardHubContext;

    public BucklingAnalysisResultsReadyNotificationConsumer(ILogger<BucklingAnalysisResultsReadyNotificationConsumer> logger, IDashboardHubContext dashboardHubContext) : base(logger)
    {
        _dashboardHubContext = dashboardHubContext;
    }

    protected override async Task ConsumeHandle(ConsumeContext<BucklingAnalysisReadyNotification> context)
    {
        await _dashboardHubContext.BucklingAnalysisFinished(context.Message.TrackingNumber);
    }
}
