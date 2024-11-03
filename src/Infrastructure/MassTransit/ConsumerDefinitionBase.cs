using MassTransit;

namespace Infrastructure.MassTransit;

public class ConsumerDefinitionBase<TConsumer> : ConsumerDefinition<TConsumer> where TConsumer : class, IConsumer
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TConsumer> consumerConfigurator, IRegistrationContext context)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);

        endpointConfigurator.UseInMemoryOutbox(context);
    }

}