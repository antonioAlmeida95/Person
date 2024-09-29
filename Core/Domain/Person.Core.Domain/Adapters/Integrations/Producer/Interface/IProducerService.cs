using Person.Core.Domain.Adapters.Integrations.Producer.Model;

namespace Person.Core.Domain.Adapters.Integrations.Producer.Interface;

public interface IProducerService
{
    Task ProduceMessage(Message message);
}