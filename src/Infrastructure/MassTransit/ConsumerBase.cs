using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.MassTransit;

public abstract class ConsumerBase<T> : IConsumer<T> where T : class
{
    private readonly ILogger _logger;

    protected ConsumerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected abstract Task ConsumeHandle(ConsumeContext<T> context);

    public async Task Consume(ConsumeContext<T> context)
    {
        try
        {
            await ConsumeHandle(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception in consumer handling message");
            throw;
        }
    }
}