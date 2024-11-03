using MassTransit;
using SagaCoordinator.Saga;

namespace SagaCoordinator.Effects;

public interface IEffect { }

public interface IEffect<TData> : IEffect where TData : class
{
    void Then(BehaviorContext<CalculationsCoordinatorState, TData> context);
    Task ThenAsync(BehaviorContext<CalculationsCoordinatorState, TData> context);
}
