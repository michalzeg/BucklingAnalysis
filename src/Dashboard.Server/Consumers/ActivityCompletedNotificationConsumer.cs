using Dashboard.Server.Hubs;
using Infrastructure.MassTransit;
using MassTransit;
using Shared.Notifications;
using Shared.Storage;

namespace Dashboard.Server.Consumers;

public class ActivityCompletedNotificationConsumer : ConsumerBase<ActivityCompletedNotification>
{
    private readonly IStorage _storage;
    private readonly IDashboardHubContext _dashboardHubContext;

    public ActivityCompletedNotificationConsumer(ILogger<ActivityCompletedNotificationConsumer> logger, IStorage storage, IDashboardHubContext dashboardHubContext) : base(logger)
    {
        _storage = storage;
        _dashboardHubContext = dashboardHubContext;
    }

    protected override async Task ConsumeHandle(ConsumeContext<ActivityCompletedNotification> context)
    {
        await _dashboardHubContext.ActivityCompleted(context.Message.TrackingNumber, context.Message.ActivityName);
    }
}

public class ActivityCompletedNotificationConsumerDefinition: ConsumerDefinitionBase<ActivityCompletedNotificationConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ActivityCompletedNotificationConsumer> consumerConfigurator, IRegistrationContext context)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);

        endpointConfigurator.PrefetchCount = 1;
    }
}
