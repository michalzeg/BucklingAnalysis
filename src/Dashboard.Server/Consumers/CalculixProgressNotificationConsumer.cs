using Dashboard.Server.Hubs;
using Infrastructure.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Notifications;

namespace Dashboard.Server.Consumers
{
    public class CalculixProgressNotificationConsumer : ConsumerBase<CalculixProgressNotification>
    {
        private readonly IDashboardHubContext _dashboardHubContext;

        public CalculixProgressNotificationConsumer(ILogger<CalculixProgressNotificationConsumer> logger, IDashboardHubContext dashboardHubContext) : base(logger)
        {
            _dashboardHubContext = dashboardHubContext;
        }

        protected override async Task ConsumeHandle(ConsumeContext<CalculixProgressNotification> context)
        {
            await _dashboardHubContext.ReportCalculixProgress(context.Message.TrackingNumber, context.Message.Progress);
        }
    }
}
