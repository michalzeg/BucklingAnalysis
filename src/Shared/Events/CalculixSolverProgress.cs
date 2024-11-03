namespace Shared.Events;

public record CalculixSolverProgress(Guid TrackingNumber, string ProgressLine) :BaseEvent(TrackingNumber);