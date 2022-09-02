namespace CQRS.Event.Abstraction;

public interface IEventBus
{
    Task PublishAsync<T>(T @event) where T : IEvent;
}