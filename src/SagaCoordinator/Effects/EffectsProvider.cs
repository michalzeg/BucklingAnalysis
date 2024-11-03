namespace SagaCoordinator.Effects;

public class EffectsProvider : IEffectsProvider
{
    private readonly Dictionary<Type, IEffect> _typeEffectMap = [];
    private readonly ILogger<IEffectsProvider> _logger;
    private readonly IServiceProvider _serviceProvider;

    public EffectsProvider(ILogger<IEffectsProvider> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void Add<TData>(IEffect effect)
    {
        _typeEffectMap.Add(typeof(TData), effect);
    }

    public void Add<TData, TEffect>()
        where TData : class
        where TEffect : IEffect<TData>
    {
        var effect = _serviceProvider.GetRequiredService<TEffect>();
        Add<TData>(effect);
    }

    public IEffect<TData> GetEffect<TData>() where TData : class
    {
        var type = typeof(TData);
        return _typeEffectMap[type] as IEffect<TData> ?? throw new ArgumentException($"Effect not registered for {type.Name}");
    }
}
