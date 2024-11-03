using Shared.Notifications;

namespace Dashboard.Server.Hubs;

public interface IDashboardHubContext
{
    Task ActivityCompleted(Guid trackingNumber, string activityName);
    Task ReportCalculixProgress(Guid trackingNumber, CalculixProgressValue progress);
    Task FacadeGenerated(Guid trackingNumber);
    Task LinearAnalysisFinished(Guid trackingNumber);
    Task BucklingAnalysisFinished(Guid trackingNumber);
    Task NonLinearAnalysisFinished(Guid trackingNumber);
}
