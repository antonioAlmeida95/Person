using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Person.Adapter.Driven.integration.Producer.Settings;
using Person.Core.Domain.Adapters.Integrations.Producer.Interface;
using Person.Core.Domain.Adapters.Integrations.Producer.Model;

namespace Person.Adapter.Driven.integration.Producer.RabbitMq;

[ExcludeFromCodeCoverage]
public class RabbitMqProducerService : IProducerService
{

    private readonly QueueSettings _queueSettings;
    private readonly ILogger<RabbitMqProducerService> _logger;
    private readonly IBus _bus;
    
    public RabbitMqProducerService(
        IOptions<QueueSettings> queueSettings,
        IBus bus,
        ILogger<RabbitMqProducerService> logger)
    {
        _queueSettings = queueSettings.Value;
        _bus = bus;
        _logger = logger;
    }
    public async Task ProduceMessage(Message message)
    {
        try
        {
            var uriQueue = $"queue:{_queueSettings.Name}";
            var queue = await _bus.GetSendEndpoint(new Uri(uriQueue));
            await queue.Send(message);
        }
        catch (Exception exception)
        {
            LogErrorException(exception);
            throw;
        }
    }

    private void LogErrorException(Exception exception) => 
        _logger.LogError(exception, "Falha ao processar mensagem");
}