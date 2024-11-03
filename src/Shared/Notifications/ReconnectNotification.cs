namespace Shared.Notifications;

public record ReconnectNotification(Guid TrackingNumber, string ConnectionId) : BaseNotification(TrackingNumber);
