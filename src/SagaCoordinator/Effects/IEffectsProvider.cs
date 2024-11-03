namespace SagaCoordinator.Effects;

public interface IEffectsProvider
{
    IEffect<TData> GetEffect<TData>() where TData : class;
}
