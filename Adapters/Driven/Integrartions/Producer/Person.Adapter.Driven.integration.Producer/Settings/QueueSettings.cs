namespace Person.Adapter.Driven.integration.Producer.Settings;

public class QueueSettings
{
    public string? Name { get; set; }
    public string? RoutingKey { get; set; }
    public bool Durable{ get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
}