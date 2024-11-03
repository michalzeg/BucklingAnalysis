namespace Shared.Notifications;

public record NonLinearAnalysisResultsReadyNotification(Guid TrackingNumber) : BaseNotification(TrackingNumber);
