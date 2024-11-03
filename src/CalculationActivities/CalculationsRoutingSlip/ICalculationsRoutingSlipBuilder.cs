using MassTransit.Courier.Contracts;

namespace CalculationActivities.CalculationsRoutingSlip;

public interface ICalculationsRoutingSlipBuilder
{
    RoutingSlip GetRoutingSlip(Guid trackingNumber);
}