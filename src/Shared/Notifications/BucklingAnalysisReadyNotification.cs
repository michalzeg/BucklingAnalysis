namespace Shared.Notifications;

public record BucklingAnalysisReadyNotification(Guid TrackingNumber) : BaseNotification(TrackingNumber);
