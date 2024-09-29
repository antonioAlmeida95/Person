namespace Person.Core.Domain.Adapters.Integrations.Producer.Model;

public class Message
{
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public string? Origem { get; set; }
    public Guid? NotificationId { get; set; }
}