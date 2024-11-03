using Infrastructure.Redis;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MassTransit;

public static class MassTransitExtensions
{

    public static IServiceCollection AddMassTransitSagaConfig<TAnchor>(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = RegisterRabbitMqSettings(services, configuration);

        services.AddSingleton(settings);
        services.AddMassTransit(config =>
        {
            config.AddSagaStateMachinesFromNamespaceContaining<TAnchor>();
            config.SetRedisSagaRepositoryProvider(e =>
            {
                var configurationString = configuration.GetRedisConnection();
                e.DatabaseConfiguration(configurationString);
                e.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                e.KeyPrefix = "saga";
                e.LockSuffix = "-lockage";
            });
            ConfigureRabbitMq(config, settings);
        });

        return services;
    }

    public static IServiceCollection AddMassTransitConsumersConfig<TAnchor>(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = RegisterRabbitMqSettings(services, configuration);

        services.AddSingleton(settings);
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<TAnchor>();
            ConfigureRabbitMq(config, settings);
        });

        return services;
    }

    public static IServiceCollection AddMassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = RegisterRabbitMqSettings(services, configuration);

        services.AddSingleton(settings);
        services.AddMassTransit(config =>
        {
            ConfigureRabbitMq(config, settings);
        });

        return services;
    }

    public static IServiceCollection AddMassTransitActivityConfig<TActivity, TArguments>(this IServiceCollection services, IConfiguration configuration)
    where TActivity : class, IExecuteActivity<TArguments> where TArguments : class
    {

        var settings = RegisterRabbitMqSettings(services, configuration);
        services.AddSingleton(settings);
        services.AddMassTransit(config =>
        {
            config.AddExecuteActivity<TActivity, TArguments>();
            ConfigureRabbitMq(config, settings);
        });

        return services;
    }

    public static IServiceCollection AddMassTransitMultipleActivityConfig(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> configurator)
    {
        var settings = RegisterRabbitMqSettings(services, configuration);
        services.AddSingleton(settings);
        services.AddMassTransit(config =>
        {
            configurator.Invoke(config);
            ConfigureRabbitMq(config, settings);
        });

        return services;
    }

    private static RabbitConfig RegisterRabbitMqSettings(IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetRabbitConfig();
        services.AddSingleton(_ => settings);
        return settings;
    }
    private static void ConfigureRabbitMq(IBusRegistrationConfigurator config, RabbitConfig settings)
    {
        config.UsingRabbitMq((context, cfg) =>
        {
           
            cfg.Host(settings.Host, settings.Port, settings.VirtualHost, x =>
            {
                x.Username(settings.Username!);
                x.Password(settings.Password!);
            });
            cfg.ConfigureEndpoints(context);
        });
    }
}