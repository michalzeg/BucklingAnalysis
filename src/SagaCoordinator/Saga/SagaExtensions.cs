using MassTransit;
using MassTransit.SagaStateMachine;
using SagaCoordinator.Effects;

namespace SagaCoordinator.Saga;

public static class SagaExtensions
{
    public static EventActivityBinder<CalculationsCoordinatorState, TData> Effect<TData>(this EventActivityBinder<CalculationsCoordinatorState, TData> binder, IEffect<TData> effect)
        where TData : class
    {
        return binder
            .Add(new ActionActivity<CalculationsCoordinatorState, TData>(effect.Then))
            .Add(new AsyncActivity<CalculationsCoordinatorState, TData>(effect.ThenAsync));
    }

    public static EventActivityBinder<CalculationsCoordinatorState, TData> Effect<TData>(this EventActivityBinder<CalculationsCoordinatorState, TData> binder, IEffectsProvider effectFactory)
        where TData : class
    {
        var effect = effectFactory.GetEffect<TData>();
        return binder.Effect(effect);
    }

}
