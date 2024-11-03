using Shared.Notifications;

namespace Dashboard.Server.Hubs;

public interface IDashboardHub
{
    Task ReportCalculixProgress(CalculixProgressValue progress);

    Task ActivityCompleted(string activityName);

    Task FacadeGenerated();

    Task LinearAnalysisFinished();

    Task BucklingAnalysisFinished();

    Task NonLinearAnalysisFinished();
}
