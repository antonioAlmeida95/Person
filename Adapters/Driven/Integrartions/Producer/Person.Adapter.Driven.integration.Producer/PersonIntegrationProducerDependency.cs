using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Person.Adapter.Driven.integration.Producer.RabbitMq;
using Person.Adapter.Driven.integration.Producer.Settings;
using Person.Core.Domain.Adapters.Integrations.Producer.Interface;

namespace Person.Adapter.Driven.integration.Producer;

public static class PersonIntegrationProducerDependency
{
    public static void AddPersonIntegrationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QueueSettings>(opt => configuration.GetSection("Apis:RabbitMq:NotificationQueue").Bind(opt));
        services.AddScoped<IProducerService, RabbitMqProducerService>();
        services.ConfigureMassTransit(configuration);
    }

    private static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var usuario = configuration.GetSection("Credentials:Apis:RabbitMqServer")["UserName"] ?? string.Empty;
        var password = configuration.GetSection("Credentials:Apis:RabbitMqServer")["Password"] ?? string.Empty;
        var host = configuration.GetSection("Credentials:Apis:RabbitMqServer")["HostName"] ?? string.Empty;

        services.AddMassTransit(x => 
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(usuario);
                    h.Password(password);
                });
                
                cfg.ConfigureEndpoints(ctx);
            })
        );
    }
}