namespace CQRS.Event.Abstraction;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent { }