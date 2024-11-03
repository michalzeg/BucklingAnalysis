using CalculationActivities.ActivityArguments;
using Dashboard.Server.Hubs;
using Infrastructure.Extensions;
using Infrastructure.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Shared.Contracts.Facade;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Consumers;

public class FacadeGeneratedNotificationConsumer : ConsumerBase<FacadeGeneratedNotification>
{
    private readonly IDashboardHubContext _dashboardHubContext;

    public FacadeGeneratedNotificationConsumer(ILogger<FacadeGeneratedNotificationConsumer> logger, IDashboardHubContext dashboardHubContext) : base(logger)
    {
        _dashboardHubContext = dashboardHubContext;
    }

    protected override async Task ConsumeHandle(ConsumeContext<FacadeGeneratedNotification> context)
    {
        await _dashboardHubContext.FacadeGenerated(context.Message.TrackingNumber);
    }
}
