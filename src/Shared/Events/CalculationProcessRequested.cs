using Shared.Contracts.Geometry;
namespace Shared.Events;

public record CalculationProcessRequested(Guid TrackingNumber, GeometryDescription Geometry, string ConnectionId) :BaseEvent(TrackingNumber);
