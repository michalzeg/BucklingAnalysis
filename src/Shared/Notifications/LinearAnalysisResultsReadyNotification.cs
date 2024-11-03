namespace Shared.Notifications;

public record LinearAnalysisResultsReadyNotification(Guid TrackingNumber) : BaseNotification(TrackingNumber);
