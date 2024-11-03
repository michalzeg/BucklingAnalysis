namespace Shared.Notifications;

public record ActivityCompletedNotification(Guid TrackingNumber, string ActivityName) :BaseNotification(TrackingNumber);
